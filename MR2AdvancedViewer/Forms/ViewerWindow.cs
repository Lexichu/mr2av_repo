using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
using MR2AdvancedViewer.Forms;
using Octokit;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.NetworkInformation;


namespace MR2AdvancedViewer
{
    public partial class ViewerWindow : Form
    {
        public ViewerWindow()
        {
            InitializeComponent();
            MR2Mode.SelectedIndex = 0;
        }

        // Some crap to do with reading memory IDK LOL I just wrote this.
        const int PROCESS_ALLACCESS = 0x1F0FFF;
        const int DXSelectionID = 4;
        const string VersionID = "0.7.1.0";
        const string ReadableVersion = "MR2 Adanced Viewer 0.7.1";
        const string ReadableVersionJP = "MF2 アドバンスド ビューアー 0.7.1";
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);
        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        public int PSXBase = 0x00000000;
        int HasRead; // I just use this so ReadProcessMemory stops complaining.
        public IntPtr psxPTR;
        public bool bJPNMode;
        Process PSXProcess;
        string EmuFileName;
        public byte[] ScratchData = new byte[4];
        public byte[] nameToWrite = new byte[24]; //bedeg

        // Loaded Memory
        private readonly int I_Exist_To_Suffer = 4; // This variable is cursed. It always returns 0 even when assigned a value...
        private int MonGR_LIF = -1;
        private int MonGR_Power = -1;
        private int MonGR_Intelligence = -1;
        private int MonGR_Skill = -1;
        private int MonGR_Speed = -1;
        private int MonGR_Defence = -1;
        public int Mon_ArenaSPD = -1;
        public int Mon_Form = 0;
        public int Mon_Age = -1;
        public int Mon_Fame = -1;
        public int Mon_LoyalSpoil = -1;
        public int Mon_OldLoyalSpoil = -1;
        public int Mon_LoyalFear = -1;
        public int Mon_OldLoyalFear = -1;
        public int Mon_NatureBase = -1;
        public int Mon_Nature = -1;
        public int Mon_EffNature = -1;
        public int Mon_Stress = -1;
        public int Mon_Fatigue = -1;
        public int MonGenus_Main = -1;
        public int MonGenus_Sub = -1;
        public int Mon_GutsRate = -1;
        public int Mon_BSpecials = -1;
        public int MonItem_Like = -1;
        public int MonItem_Dislike = -1;
        public int Mon_Lifespan = -1;
        public int Mon_OldLifespan = -1;
        public int Mon_InitLifespan = -1;
        public int Mon_PlayType = -1;
        public int Mon_LifeStage = -1;
        public int Mon_LifeType = -1;
        public int Mon_CupJellies = -1;
        public int Mon_Rank = -1;
        public int Mon_Lif, Mon_Pow, Mon_Int, Mon_Spd, Mon_Skl, Mon_Def;
        public float Mon_EffSpd, Mon_EffDef;
        public int Mon_Drug, Mon_DrugDuration, Mon_OldDrug;
        public int Mon_TrainBoost;
        public int[] Mon_Moves = new int[24];
        public int[] Mon_MoveUsed = new int[24];
        public bool[] Con1Input = new bool[16];
        public int[] ItemList = new int[20];
        public int BananaCount, OldBananaCount;
        public int P1Input;
        public int Game_NextSale;
        public int Game_ErrantryCD;
        public int Mon_PrizeMoney;
        public int[] ActiveMoves = new int[4];

        // Motivation Values.
        public int Mon_MotivDom = -1;
        public int Mon_MotivStu = -1;
        public int Mon_MotivRun = -1;
        public int Mon_MotivSho = -1;
        public int Mon_MotivDod = -1;
        public int Mon_MotivEnd = -1;
        public int Mon_MotivPul = -1;
        public int Mon_MotivMed = -1;
        public int Mon_MotivLea = -1;
        public int Mon_MotivSwi = -1;

        // Bool Memory Values
        public bool Mon_ItemUsed = false;
        public bool MonPeach_Gold = false;
        public bool MonPeach_Silver = false;

        // Internal Variables
        public int pleasestoppokingme;
        public int EmuVer = -1;
        public bool bViewingMR2 = false;
        public bool emuLoaded = false;
        public bool AgeWeeksOnly;
        public bool RawNatureMod;
        public bool bShowingExtras;
        public int BananaTicks;
        public int UnfreezeTicks;
        public string interVER;
        public MonLifeIndexWindow LIW;
        public MonMoveWindow MMW;
        public MR2ControllerView cv;
        public TrainingWindow TW;
        public ItemLister il;
        public MRDebug mDBG;
        readonly System.Media.SoundPlayer BananaWin = new System.Media.SoundPlayer(Properties.Resources.MR2_Banana_Success);
        readonly System.Media.SoundPlayer BananaLose = new System.Media.SoundPlayer(Properties.Resources.MR2_Banana_Fail);
        public Random rng = new Random();
        public int RNGNew, RNGCur = -1;
        public bool bChangingName = false; //bedeg
        public string prevName; //bedeg

        public bool IsConnectedToInternet()
        {
            if (!NetworkInterface.GetIsNetworkAvailable()) // Do we even have a network device?
            {
                return false;
            }

            Uri host = new Uri("https://github.com/Lexichu/mr2av_repo"); // Does the AV's repo respond?
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(host.Host, 3000);
                if (reply.Status == IPStatus.Success)
                    return true;
            }
            catch { }
            return false;
        }

        private async Task CheckGitHubNewerVersion()
        {
            //Get all releases from GitHub
            //Source: https://octokitnet.readthedocs.io/en/latest/getting-started/

            if (!IsConnectedToInternet())
            {
                MessageBox.Show(@"Either GitHub is down, or you are offline. Auto-update check cannot proceed.

Check your connection, but if GitHub is down then disregard this message.", "Auto-update Check Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            GitHubClient client = new GitHubClient(new ProductHeaderValue("mr2av_repo"));
            IReadOnlyList<Release> releases = await client.Repository.Release.GetAll("Lexichu", "mr2av_repo");

            //Setup the versions
            Version latestGitHubVersion = new Version(releases[0].TagName);
            Version localVersion = new Version(VersionID); //Replace this with your local version.

            //Compare the Versions
            //Source: https://stackoverflow.com/questions/7568147/compare-version-numbers-without-using-split-function
            int versionComparison = localVersion.CompareTo(latestGitHubVersion);
            if (versionComparison < 0)
            {
                DialogResult result = MessageBox.Show(@"You are currently on an outdated build of MR2AV.

Please visit https://github.com/Lexichu/mr2av_repo/releases/ to download the latest release!
(clicking OK will close MR2AV and visit the GitHub)", "MR2AV Version Notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    Process.Start("https://github.com/Lexichu/mr2av_repo/releases/");
                    this.Close();
                }
                else
                {
                }
            }
            else if (versionComparison > 0)
            {
                // Do something for being ahead of the curve??
            }
            else
            {
                // Congrats; you're on the latest version!
            }
        }

        public string MonGRAlphabetise(int gValue, TextBox BoxID)
        {
            switch (gValue)
            {
                case 0: BoxID.BackColor = Color.OrangeRed; return "E";
                case 1: BoxID.BackColor = Color.Orange; return "D";
                case 2: BoxID.BackColor = Color.Yellow; return "C";
                case 3: BoxID.BackColor = Color.YellowGreen; return "B";
                case 4: BoxID.BackColor = Color.LimeGreen; return "A";
                case 5: BoxID.BackColor = Color.HotPink; return "S";
                case 6: BoxID.BackColor = Color.HotPink; return "S2";
                case 7: BoxID.BackColor = Color.HotPink; return "S3";
                case 8: BoxID.BackColor = Color.HotPink; return "S4";
                case 9: BoxID.BackColor = Color.HotPink; return "S5";
                case 10: BoxID.BackColor = Color.HotPink; return "F";
                default: BoxID.BackColor = SystemColors.Control; return "---";
            }
        }

        public string CycleMessages()
        {
            while (RNGNew == RNGCur)
            {
                RNGNew = rng.Next(1, 5);
            }
            RNGCur = RNGNew;
            switch (RNGNew)
            {
                case 1:
                    StatusBarURL.Text = "https://twitch.tv/lexichu_";
                    return "Visit Lexi's Twitch channel; leave her a follow! :D";
                case 2:
                    StatusBarURL.Text = "https://legendcup.com";
                    return "Want more info on Monster Rancher? Check out LegendCup!";
                case 3:
                    StatusBarURL.Text = ">//w//<";
                    return "If you want to donate to Lexi, click the blushyface... ";
                case 4:
                    StatusBarURL.Text = "まとめwiki [MF2 Wiki]";
                    return "日本人？ いいですよ！日本語のガイド ＠";
                case 5:
                    StatusBarURL.Text = "https://twitch.tv/lexichu_";
                    return "Visit Lexi's Twitch channel; leave her a follow! :)";
            }
            StatusBarURL.Text = "https://twitch.tv/lexichu_";
            return "Visit Lexi's Twitch channel; leave her a follow! :)";
        }

        public bool PossibleCocoon()
        {
            if (MonGenus_Main == 4 && MonGenus_Sub == 4 && (MonGR_LIF != 3 || MonGR_Power != 3 || MonGR_Intelligence != 0 || MonGR_Skill != 1 || MonGR_Speed != 1 || MonGR_Defence != 3))
                return true;
            else if (MonGenus_Sub == 36)
            {
                switch (MonGenus_Main)
                {
                    case 0:
                    case 13:
                    case 15:
                    case 23:
                    case 28:
                    case 34:
                    case 37:
                        return true;
                    default:
                        return false;
                }
            }
            return false;
        }

        public string MonBreedNames()
        {
            // Special Cases
            if ((MonGenus_Main == -1 || MonGenus_Sub == -1) || (MonGenus_Main == 0 && MonGenus_Sub == 0 && Mon_GutsRate == 0) || (MonGenus_Main == 46 && MonGenus_Sub == 46 && Mon_GutsRate == 0)) // No monster/no loaded data.
            {
                MonGutsRateBox.BackColor = SystemColors.Control;
                return "No Monster";
            }
            if (Mon_GutsRate == 0 && (MonGR_LIF == 0 || MonGR_Power == 0 || MonGR_Intelligence == 0 || MonGR_Skill == 0 || MonGR_Speed == 0 || MonGR_Defence == 0))
            {
                MonGutsRateBox.BackColor = SystemColors.Control;
                return "No Monster";
            }
            if (MonGenus_Main == 5 && MonGenus_Sub == 15) // Slated Henger/Gali revived
            {
                MonGutsRateBox.BackColor = SystemColors.Control;
                if (EmuVer == DXSelectionID)
                    return "Protomessiah";
                else
                    return "「プロト」の力わからないwww!";
            }
            if (PossibleCocoon() && MonWormGenus() != "Undefined Monster")
                return MonWormGenus();
            if (Mon_GutsRate < 6)
                MonGutsRateBox.BackColor = Color.HotPink; // Nothing has more than 5 Guts/Second.

            switch (MonGenus_Main)
            {
                case 0: // Pixies
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 7)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 0 && MonGR_Power == 1 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Pixie";
                            else
                                return "Pixie (N/S)";
                        case 1:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Daina";
                            else
                                return "Daina (N/S)";
                        case 2:
                            if (Mon_GutsRate == 9)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Unico";
                            else
                                return "Unico (N/S)";
                        case 6:
                            if (Mon_GutsRate == 8)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 4 && MonGR_Skill == 2 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Jilt";
                            else
                                return "Jilt (N/S)";
                        case 7:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Granity";
                            else
                                return "Granity (N/S)";
                        case 8:
                            if (Mon_GutsRate == 9)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 1)
                                return "Dixie";
                            else
                                return "Dixie (N/S)";
                        case 9:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Janne";
                            else
                                return "Janne (N/S)";
                        case 11:
                            if (Mon_GutsRate == 8)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 0 && MonGR_Power == 1 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Mint";
                            else
                                return "Mint (N/S)";
                        case 13:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Lepus";
                            else
                                return "Lepus (N/S)";
                        case 15:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 0 && MonGR_Power == 1 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Angel";
                            else
                                return "Angel (N/S)";
                        case 16:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 0 && MonGR_Power == 1 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Kitten";
                            else
                                return "Kitten (N/S)";
                        case 18:
                            if (Mon_GutsRate == 9)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Jinnie";
                            else
                                return "Jinnie (N/S)";
                        case 22:
                            if (Mon_GutsRate == 7)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Futurity";
                            else
                                return "Futurity (N/S)";
                        case 23:
                            if (Mon_GutsRate == 9)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 0 && MonGR_Power == 1 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Vanity";
                            else
                                return "Vanity (N/S)";
                        case 24:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 4 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Snowy";
                            else
                                return "Snowy (N/S)";
                        case 26:
                            if (Mon_GutsRate == 9)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Lilim";
                            else
                                return "Lilim (N/S)";
                        case 28:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Nagisa";
                            else
                                return "Nagisa (N/S)";
                        case 31:
                            if (Mon_GutsRate == 9)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 0 && MonGR_Power == 1 && MonGR_Intelligence == 4 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Dryad";
                            else
                                return "Dryad (N/S)";
                        case 33:
                            if (Mon_GutsRate == 7)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Serenity";
                            else
                                return "Serenity (N/S)";
                        case 34:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 0 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Silhouette";
                            else
                                return "Silhouette (N/S)";
                        case 36:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Night Flyer";
                            else
                                return "Night Flyer (N/S)";
                        case 37:
                            if (Mon_GutsRate == 9)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Allure";
                            else
                                return "Allure (N/S)";
                        case 38:
                            if (Mon_GutsRate == 7)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 0 && MonGR_Power == 1 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Poison (DNA Capsule)";
                            else
                                return "Poison";
                        case 39:
                            return "Kasumi";
                        case 40:
                            if (Mon_GutsRate == 7)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 0 && MonGR_Power == 1 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Mia (DNA Capsule)";
                            else
                                return "Mia";
                        default:
                            return "Game Crash";
                    }
                case 1: // D! For Dragon!!
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 3 && MonGR_Intelligence == 4 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Tiamat";
                            else
                                return "Tiamat (N/S)";
                        case 1:
                            if (Mon_GutsRate == 19)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 4 && MonGR_Intelligence == 4 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Dragon";
                            else
                                return "Dragon (N/S)";
                        case 4:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 4 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Corkasus";
                            else
                                return "Corkasus (N/S)";
                        case 5:
                            if (Mon_GutsRate == 18)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 4 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Tecno Dragon";
                            else
                                return "Tecno Dragon (N/S)";
                        case 7:
                            if (Mon_GutsRate == 19)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 4 && MonGR_Intelligence == 3 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Stone Dragon";
                            else
                                return "Stone Dragon (N/S)";
                        case 9:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 4 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Armor Dragon";
                            else
                                return "Armor Dragon (N/S)";
                        case 10:
                            if (Mon_GutsRate == 18)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Crab Dragon";
                            else
                                return "Crab Dragon (N/S)";
                        case 11:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Hound Dragon";
                            else
                                return "Hound Dragon (N/S)";
                        case 15:
                            if (Mon_GutsRate == 18)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 4 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Gariel";
                            else
                                return "Gariel (N/S)";
                        case 16:
                            if (Mon_GutsRate == 18)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 4 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Oscerot";
                            else
                                return "Oscerot (N/S)";
                        case 18:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 4 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Dodongo";
                            else
                                return "Dodongo (N/S)";
                        case 22:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Gidras";
                            else
                                return "Gidras (N/S)";
                        case 26:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Death Dragon";
                            else
                                return "Death Dragon (N/S)";
                        case 34:
                            if (Mon_GutsRate == 19)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Ragnaroks";
                            else
                                return "Ragnaroks (N/S)";
                        case 38: // Jesus christ, Mu!
                            if (Mon_GutsRate == 19)
                            {
                                MonGutsRateBox.BackColor = SystemColors.Control;
                                if (MonGR_LIF == 2 && MonGR_Power == 4 && MonGR_Intelligence == 4 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                    return "Moo (DNA Capsule)";
                                else
                                    return "Moo (N/S)";
                            }
                            else if (Mon_GutsRate == 18)
                            {
                                MonGutsRateBox.BackColor = SystemColors.Control;
                                if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 0 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                    return "Moo (CD)";
                                else
                                    return "Moo (N/S)";
                            }
                            else if (Mon_GutsRate == 16)
                            {
                                MonGutsRateBox.BackColor = SystemColors.Control;
                                if (MonGR_LIF == 4 && MonGR_Power == 1 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                    return "Muu (Hard Mode CD)";
                                else
                                    return "Moo (N/S)";
                            }
                            else if (Mon_GutsRate == 20)
                            {
                                MonGutsRateBox.BackColor = SystemColors.Control;
                                if (MonGR_LIF == 3 && MonGR_Power == 4 && MonGR_Intelligence == 4 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 3)
                                    return "Muu (HM DNA Capsule)";
                                else
                                    return "Moo (N/S)";
                            }
                            else
                            {
                                MonGutsRateBox.BackColor = Color.HotPink;
                                return "Moo (N/S)";
                            }
                        case 39:
                            return "[E] Magma Heart";
                        default:
                            return "Game Crash";
                    }
                case 2: // Centaurs
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 4 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Ferious";
                            else
                                return "Ferious (N/S)";
                        case 1:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Dragoon";
                            else
                                return "Dragoon (N/S)";
                        case 2:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 4 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Centaur";
                            else
                                return "Centaur (N/S)";
                        case 7:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Trojan";
                            else
                                return "Trojan (N/S)";
                        case 9:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Chariot";
                            else
                                return "Chariot (N/S)";
                        case 10:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Antares";
                            else
                                return "Antares (N/S)";
                        case 11:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 4 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Celious";
                            else
                                return "Celious (N/S)";
                        case 18:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 4 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Bazoo";
                            else
                                return "Bazoo (N/S)";
                        case 26:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 4 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Reaper";
                            else
                                return "Reaper (N/S)";
                        case 38:
                            return "Trotter";
                        case 39:
                            return "Blue Thunder";
                        case 40:
                            return "[E] Sniper";
                        default:
                            return "Game Crash";
                    }
                case 3: // Human Centipedes
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Peach Tree Bug";
                            else
                                return "Peach Tree Bug (N/S)";
                        case 3:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 4 && MonGR_Power == 1 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Color Pandora";
                            else
                                return "Color Pandora (N/S)";
                        case 28:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Liquid Cube";
                            else
                                return "Liquid Cube (N/S)";
                        case 38:
                            return "Dice";
                        case 39:
                            return "Tram";
                        default:
                            return "Game Crash";
                    }
                case 4: // Big Bug Bois
                    switch (MonGenus_Sub)
                    {
                        case 1:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Bethelgeus";
                            else
                                return "Bethelgeus (N/S)";
                        case 4:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 0 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Beaclon";
                            //else if(MonIsCocooned())
                            //return "Beaclon (Cocoon: W/W)"
                            else
                                return "Beaclon (N/S)";
                        case 5:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Melcarba";
                            else
                                return "Melcarba (N/S)";
                        case 7:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Rocklon";
                            else
                                return "Rocklon (N/S)";
                        case 9:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Centurion";
                            else
                                return "Centurion (N/S)";
                        case 11:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Sloth Beetle";
                            else
                                return "Sloth Beetle (N/S)";
                        case 18:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 0 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Kaut Roar Kaut";
                            else
                                return "Kaut Roar Kaut (N/S)";
                        case 26:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Jaggernaut";
                            else
                                return "Jaggernaut (N/S)";
                        case 32:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Ducklon";
                            else
                                return "Ducklon (N/S)";
                        case 38:
                            return "Eggplantern";
                        default:
                            return "Game Crash";
                    }
                case 5: // Children of Stardust Wyvern
                    switch (MonGenus_Sub)
                    {
                        case 1:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 3 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Garlant";
                            else
                                return "Garlant (N/S)";
                        case 5:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 1)
                                return "Henger";
                            else
                                return "Henger (N/S)";
                        case 7:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Gaia";
                            else
                                return "Gaia (N/S)";
                        case 8:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 1)
                                return "Omega";
                            else
                                return "Omega (N/S)";
                        case 22:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Heuy";
                            else
                                return "Heuy (N/S)";
                        case 26:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 3 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "End Bringer";
                            else
                                return "End Bringer (N/S)";
                        case 31:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Automaton";
                            else
                                return "Automaton (N/S)";
                        case 34:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Black Henger";
                            else
                                return "Black Henger (N/S)";
                        case 38:
                            return "Skeleton";
                        default:
                            return "Game Crash";
                    }
                case 6: // Chucky
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 8)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 0 && MonGR_Intelligence == 3 && MonGR_Skill == 1 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Baby Doll";
                            else
                                return "Baby Doll (N/S)";
                        case 1:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 1 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Draco Doll";
                            else
                                return "Draco Doll (N/S)";
                        case 5:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 1 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Henger Doll";
                            else
                                return "Henger Doll (N/S)";
                        case 6:
                            if (Mon_GutsRate == 9)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 0 && MonGR_Intelligence == 3 && MonGR_Skill == 0 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Wracky";
                            else
                                return "Wracky (N/S)";
                        case 7:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 0 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Pebbly";
                            else
                                return "Pebbly (N/S)";
                        case 9:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 1 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Petit Knight";
                            else
                                return "Petit Knight (N/S)";
                        case 18:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Bakky";
                            else
                                return "Bakky (N/S)";
                        case 22:
                            if (Mon_GutsRate == 8)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 0 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Metal Glay";
                            else
                                return "Metal Glay (N/S)";
                        case 26:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Tricker";
                            else
                                return "Tricker (N/S)";
                        case 31:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 0 && MonGR_Intelligence == 3 && MonGR_Skill == 0 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Mocky";
                            else
                                return "Mocky (N/S)";
                        case 38:
                            return "Satan Clause";
                        default:
                            return "Game Crash";
                    }
                case 7: // Golem
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 3 && MonGR_Intelligence == 3 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Pink Golem";
                            else
                                return "Pink Golem (N/S)";
                        case 1:
                            if (Mon_GutsRate == 18)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 4 && MonGR_Intelligence == 3 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 3)
                                return "Tyrant";
                            else
                                return "Tyrant (N/S)";
                        case 4:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 4 && MonGR_Intelligence == 1 && MonGR_Skill == 0 && MonGR_Speed == 0 && MonGR_Defence == 4)
                                return "Strong Horn";
                            else
                                return "Strong Horn (N/S)";
                        case 5:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 4 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Gobi";
                            else
                                return "Gobi (N/S)";
                        case 6:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 0 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Mariomax";
                            else
                                return "Mariomax (N/S)";
                        case 7:
                            if (Mon_GutsRate == 18)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 4 && MonGR_Intelligence == 2 && MonGR_Skill == 0 && MonGR_Speed == 0 && MonGR_Defence == 4)
                                return "Golem";
                            else
                                return "Golem (N/S)";
                        case 8:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Scaled Golem";
                            else
                                return "Scaled Golem (N/S)";
                        case 9:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 4 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 4)
                                return "Battle Rocks";
                            else
                                return "Battle Rocks (N/S)";
                        case 10:
                            if (Mon_GutsRate == 18)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 4)
                                return "Dagon";
                            else
                                return "Dagon (N/S)";
                        case 11:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Big Blue";
                            else
                                return "Big Blue (N/S)";
                        case 13:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 4 && MonGR_Intelligence == 1 && MonGR_Skill == 1 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Moaigon";
                            else
                                return "Moaigon (N/S)";
                        case 14:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 4 && MonGR_Intelligence == 1 && MonGR_Skill == 0 && MonGR_Speed == 0 && MonGR_Defence == 3)
                                return "Sleepyhead";
                            else
                                return "Sleepyhead (N/S)";
                        case 15:
                            if (Mon_GutsRate == 18)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 4 && MonGR_Intelligence == 2 && MonGR_Skill == 0 && MonGR_Speed == 0 && MonGR_Defence == 3)
                                return "Amenhotep";
                            else
                                return "Amenhotep (N/S)";
                        case 17:
                            if (Mon_GutsRate == 18)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 3 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 3)
                                return "Pressure";
                            else
                                return "Pressure (N/S)";
                        case 18:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 4 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Dao";
                            else
                                return "Dao (N/S)";
                        case 22:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 4)
                                return "Astro";
                            else
                                return "Astro (N/S)";
                        case 23:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 3 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 3)
                                return "Titan";
                            else
                                return "Titan (N/S)";
                        case 26:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 3)
                                return "Angolmor";
                            else
                                return "Angolmor (N/S)";
                        case 28:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 4)
                                return "Poseidon";
                            else
                                return "Poseidon (N/S)";
                        case 31:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 3 && MonGR_Intelligence == 3 && MonGR_Skill == 0 && MonGR_Speed == 0 && MonGR_Defence == 3)
                                return "Wood Golem";
                            else
                                return "Wood Golem (N/S)";
                        case 33:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 2)
                                return "Ecologuardian";
                            else
                                return "Ecologuardian (N/S)";
                        case 34:
                            if (Mon_GutsRate == 18)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 0 && MonGR_Speed == 0 && MonGR_Defence == 4)
                                return "Black Golem";
                            else
                                return "Black Golem (N/S)";
                        case 36:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 3)
                                return "Magna";
                            else
                                return "Magna (N/S)";
                        case 37:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 4 && MonGR_Intelligence == 1 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 3)
                                return "Marble Guy";
                            else
                                return "Marble Guy (N/S)";
                        case 38:
                            if (Mon_GutsRate == 18)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 4 && MonGR_Intelligence == 2 && MonGR_Skill == 0 && MonGR_Speed == 0 && MonGR_Defence == 4)
                                return "Forward Golem (DNA C.)";
                            else
                                return "Forward Golem (N/S)";
                        case 39:
                            return "[E] Sand Golem";
                        default:
                            return "Game Crash";
                    }
                case 8: // Fake Dinos
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Fairy Saurian";
                            else
                                return "Fairy Saurian (N/S)";
                        case 1:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Salamander";
                            else
                                return "Salamander (N/S)";
                        case 7:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Rock Saurian";
                            else
                                return "Rock Saurian (N/S)";
                        case 8:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Zuum";
                            else
                                return "Zuum (N/S)";
                        case 10:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 3)
                                return "Crab Saurian";
                            else
                                return "Crab Saurian (N/S)";
                        case 11:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Hound Saurian";
                            else
                                return "Hound Saurian (N/S)";
                        case 13:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 1)
                                return "Spot Saurian";
                            else
                                return "Spot Saurian (N/S)";
                        case 14:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Hachiro";
                            else
                                return "Hachiro (N/S)";
                        case 15:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Noble Saurian";
                            else
                                return "Noble Saurian (N/S)";
                        case 16:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 2)
                                return "Tasman";
                            else
                                return "Tasman (N/S)";
                        case 18:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Sand Saurian";
                            else
                                return "Sand Saurian (N/S)";
                        case 23:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Mustardy";
                            else
                                return "Mustardy (N/S)";
                        case 26:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Basilisk";
                            else
                                return "Basilisk (N/S)";
                        case 28:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Jelly Saurian";
                            else
                                return "Jelly Saurian (N/S)";
                        case 31:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Wood Saurian";
                            else
                                return "Wood Saurian (N/S)";
                        case 33:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 1 && MonGR_Intelligence == 1 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Aloha Saurian";
                            else
                                return "Aloha Saurian (N/S)";
                        case 34:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Black Saurian";
                            else
                                return "Black Saurian (N/S)";
                        case 36:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Shell Saurian";
                            else
                                return "Shell Saurian (N/S)";
                        case 37:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Naga Saurian";
                            else
                                return "Naga Saurian (N/S)";
                        case 38:
                            return "Zebra Saurian";
                        case 39:
                            return "[E] Wild Saurian";
                        default:
                            return "Game Crash";
                    }
                case 9: // Machi = Besto Waifu(s)
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Leziena";
                            else
                                return "Leziena (N/S)";
                        case 1:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 3)
                                return "Vesuvius";
                            else
                                return "Vesuvius (N/S)";
                        case 4:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 4)
                                return "Hercules";
                            else
                                return "Hercules (N/S)";
                        case 7:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 4)
                                return "Kelmadics";
                            else
                                return "Kelmadics (N/S)";
                        case 9:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 4)
                                return "Durahan";
                            else
                                return "Durahan (N/S)";
                        case 10:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 4)
                                return "Lorica";
                            else
                                return "Lorica (N/S)";
                        case 11:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Hound Knight";
                            else
                                return "Hound Knight (N/S)";
                        case 20:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Garuda";
                            else
                                return "Garuda (N/S)";
                        case 22:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 3 && MonGR_Speed == 0 && MonGR_Defence == 4)
                                return "Metal Glory";
                            else
                                return "Metal Glory (N/S)";
                        case 26:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 0 && MonGR_Defence == 3)
                                return "Genocider";
                            else
                                return "Genocider (N/S)";
                        case 31:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 3)
                                return "Wood Knight";
                            else
                                return "Wood Knight (N/S)";
                        case 38:
                            return "Shogun";
                        case 39:
                            return "Ruby Knight";
                        case 40:
                            return "Kokushi Muso";
                        default:
                            return "Game Crash";
                    }
                case 10: // Crabbos
                    switch (MonGenus_Sub)
                    {
                        case 5:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 3)
                                return "Renocraft";
                            else
                                return "Renocraft (N/S)";
                        case 7:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 4)
                                return "Priarocks";
                            else
                                return "Priarocks (N/S)";
                        case 9:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 4)
                                return "Plated Arrow";
                            else
                                return "Plated Arrow (N/S)";
                        case 10:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 4)
                                return "Arrow Head";
                            else
                                return "Arrow Head (N/S)";
                        case 23:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Mustard Arrow";
                            else
                                return "Mustard Arrow (N/S)";
                        case 26:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Selketo";
                            else
                                return "Selketo (N/S)";
                        case 31:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Log Sawer";
                            else
                                return "Log Sawer (N/S)";
                        case 38:
                            return "Sumopion";
                        case 39:
                            return "[E] Silver Face";
                        default:
                            return "Game Crash";
                    }
                case 11: // Doggos
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 8)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 4 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Daton";
                            else
                                return "Daton (N/S)";
                        case 7:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Rock Hound";
                            else
                                return "Rock Hound (N/S)";
                        case 8:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 4 && MonGR_Speed == 3 && MonGR_Defence == 1)
                                return "Datonare";
                            else
                                return "Datonare (N/S)";
                        case 11:
                            if (Mon_GutsRate == 9)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 4 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Tiger";
                            else
                                return "Tiger (N/S)";
                        case 13:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Hare Hound";
                            else
                                return "Hare Hound (N/S)";
                        case 15:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Balon";
                            else
                                return "Balon (N/S)";
                        case 23:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 4 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Mono Eyed";
                            else
                                return "Mono Eyed (N/S)";
                        case 28:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 4 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Jelly Hound";
                            else
                                return "Jelly Hound (N/S)";
                        case 33:
                            if (Mon_GutsRate == 9)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Tropical Dog";
                            else
                                return "Tropical Dog (N/S)";
                        case 34:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Terror Dog";
                            else
                                return "Terror Dog (N/S)";
                        case 36:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Jagd Hound";
                            else
                                return "Jagd Hound (N/S)";
                        case 37:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 4 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Cabalos";
                            else
                                return "Cabalos (N/S)";
                        case 38:
                            return "White Hound";
                        case 39:
                            return "[E] Kamui";
                        default:
                            return "Undefined Monster";
                    }
                case 12: // Byoin! Byoin! Timer Scum no yoni!
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 8)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 4 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Fairy Hopper";
                            else
                                return "Fairy Hopper (N/S)";
                        case 1:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 3 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Draco Hopper";
                            else
                                return "Draco Hopper (N/S)";
                        case 11:
                            if (Mon_GutsRate == 8)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 4 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Skipper";
                            else
                                return "Skipper (N/S)";
                        case 12:
                            if (Mon_GutsRate == 8)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 4 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Hopper";
                            else
                                return "Hopper (N/S)";
                        case 16:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Mustachios";
                            else
                                return "Mustachios (N/S)";
                        case 18:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 4 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Emerald Eye";
                            else
                                return "Emerald Eye (N/S)";
                        case 22:
                            if (Mon_GutsRate == 7)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 4 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Springer";
                            else
                                return "Springer (N/S)";
                        case 23:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 4 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Rear Eyed";
                            else
                                return "Rear Eyed (N/S)";
                        case 24:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Snow Hopper";
                            else
                                return "Snow Hopper (N/S)";
                        case 25:
                            if (Mon_GutsRate == 9)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 4 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Pink Hopper";
                            else
                                return "Pink Hopper (N/S)";
                        case 26:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 4 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Sneak Hopper";
                            else
                                return "Sneak Hopper (N/S)";
                        case 31:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Woody Hopper";
                            else
                                return "Woody Hopper (N/S)";
                        case 38:
                            return "Frog Hopper";
                        case 39:
                            return "[E] Bloody Eye";
                        default:
                            return "Undefined Monster";
                    }
                case 13: // Bun-buns!
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 0 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 4 && MonGR_Defence == 0)
                            {
                                if (Mon_GutsRate == 125)
                                    return "Fairy Hare (MF2 v1.0)";
                                else
                                    return "Fairy Hare";
                            }
                            else
                                return "Fairy Hare (N/S)";
                        case 7:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 4 && MonGR_Intelligence == 1 && MonGR_Skill == 1 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Rocky Fur";
                            else
                                return "Rocky Fur (N/S)";
                        case 8:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 0 && MonGR_Skill == 2 && MonGR_Speed == 3 && MonGR_Defence == 1)
                                return "Scaled Hare";
                            else
                                return "Scaled Hare (N/S)";
                        case 11:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 3 && MonGR_Speed == 4 && MonGR_Defence == 0)
                                return "Blue Hare";
                            else
                                return "Blue Hare (N/S)";
                        case 13:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 4 && MonGR_Intelligence == 0 && MonGR_Skill == 2 && MonGR_Speed == 4 && MonGR_Defence == 0)
                                return "Hare";
                            else
                                return "Hare (N/S)";
                        case 15:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 3 && MonGR_Defence == 1)
                                return "Prince Hare";
                            else
                                return "Prince Hare (N/S)";
                        case 23:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Four Eyed";
                            else
                                return "Four Eyed (N/S)";
                        case 28:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 3 && MonGR_Defence == 1)
                                return "Jelly Hare";
                            else
                                return "Jelly Hare (N/S)";
                        case 33:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Leaf Hare";
                            else
                                return "Leaf Hare (N/S)";
                        case 34:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Evil Hare";
                            else
                                return "Evil Hare (N/S)";
                        case 36:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Wild Hare";
                            else
                                return "Wild Hare (N/S)";
                        case 37:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 4 && MonGR_Intelligence == 0 && MonGR_Skill == 2 && MonGR_Speed == 3 && MonGR_Defence == 1)
                                return "Purple Hare";
                            else
                                return "Purple Hare (N/S)";
                        case 38:
                            return "Kung Fu Hare";
                        default:
                            return "Undefined Monster";
                    }
                case 14: // Derpy Dogs
                    switch (MonGenus_Sub)
                    {
                        case 1:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 2)
                                return "Magmax";
                            else
                                return "Magmax (N/S)";
                        case 7:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 3)
                                return "Higante";
                            else
                                return "Higante (N/S)";
                        case 9:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 3)
                                return "War Baku";
                            else
                                return "War Baku (N/S)";
                        case 11:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Icebergy";
                            else
                                return "Icebergy (N/S)";
                        case 13:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 0 && MonGR_Skill == 1 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Gontar";
                            else
                                return "Gontar (N/S)";
                        case 14:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 4 && MonGR_Power == 3 && MonGR_Intelligence == 0 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 2)
                                return "Baku";
                            else
                                return "Baku (N/S)";
                        case 16:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Nussie";
                            else
                                return "Nussie (N/S)";
                        case 26:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 2)
                                return "Baku Clown";
                            else
                                return "Baku Clown (N/S)";
                        case 28:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 2)
                                return "Giga Pint";
                            else
                                return "Giga Pint (N/S)";
                        case 38:
                            return "Shishi";
                        default:
                            return "Undefined Monster";
                    }
                case 15: // PRAISE THE SUN
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 4 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Pink Mask";
                            else
                                return "Pink Mask (N/S)";
                        case 7:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 3 && MonGR_Intelligence == 3 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Stone Mask";
                            else
                                return "Stone Mask (N/S)";
                        case 8:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Scaled Mask";
                            else
                                return "Scaled Mask (N/S)";
                        case 11:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Fanged Mask";
                            else
                                return "Fanged Mask (N/S)";
                        case 13:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Furred Mask";
                            else
                                return "Furred Mask (N/S)";
                        case 15:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 3 && MonGR_Intelligence == 4 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Gali";
                            else
                                return "Gali (N/S)";
                        case 23:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 4 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Suezo Mask";
                            else
                                return "Suezo Mask (N/S)";
                        case 28:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 4 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Aqua Mask";
                            else
                                return "Aqua Mask (N/S)";
                        case 33:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Colorful";
                            else
                                return "Colorful (N/S)";
                        case 34:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Galirous";
                            else
                                return "Galirous (N/S)";
                        case 36:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Brown Mask";
                            else
                                return "Brown Mask (N/S)";
                        case 37:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Purple Mask";
                            else
                                return "Purple Mask (N/S)";
                        case 38:
                            return "Happy Mask";
                        default:
                            return "Undefined Monster";
                    }
                case 16: // Victims of Censorship
                    switch (MonGenus_Sub)
                    {
                        case 1:
                            if (Mon_GutsRate == 18)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 4 && MonGR_Skill == 2 && MonGR_Speed == 3 && MonGR_Defence == 1)
                                return "Draco Kato";
                            else
                                return "Draco Kato (N/S)";
                        case 11:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 0 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 4 && MonGR_Defence == 1)
                                return "Blue Kato";
                            else
                                return "Blue Kato (N/S)";
                        case 15:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 4 && MonGR_Skill == 2 && MonGR_Speed == 3 && MonGR_Defence == 1)
                                return "Gordish";
                            else
                                return "Gordish (N/S)";
                        case 16:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 0 && MonGR_Intelligence == 4 && MonGR_Skill == 2 && MonGR_Speed == 4 && MonGR_Defence == 1)
                                return "Kato";
                            else
                                return "Kato (N/S)";
                        case 23:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 4 && MonGR_Defence == 2)
                                return "Citronie";
                            else
                                return "Citronie (N/S)";
                        case 25:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 1)
                                return "Pink Kato";
                            else
                                return "Pink Kato (N/S)";
                        case 26:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 4 && MonGR_Defence == 2)
                                return "Tainted Cat";
                            else
                                return "Tainted Cat (N/S)";
                        case 38:
                            return "Ninja Kato";
                        case 39:
                            return "[E] Crescent";
                        default:
                            return "Undefined Monster";
                    }
                case 17: // SUGAR BBW and co
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Pink Zilla";
                            else
                                return "Pink Zilla (N/S)";
                        case 11:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Gooji";
                            else
                                return "Gooji (N/S)";
                        case 17:
                            if (Mon_GutsRate == 18)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 4 && MonGR_Intelligence == 1 && MonGR_Skill == 0 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Zilla";
                            else
                                return "Zilla (N/S)";
                        case 28:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Gigalon";
                            else
                                return "Gigalon (N/S)";
                        case 38:
                            return "Deluxe Liner";
                        case 39:
                            return "[E] Zilla King";
                        default:
                            return "Undefined Monster";
                    }
                case 18: // Potheads.
                    switch (MonGenus_Sub)
                    {
                        case 18:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Bajarl";
                            else
                                return "Bajarl (N/S)";
                        case 26:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Jaba";
                            else
                                return "Jaba (N/S)";
                        case 38:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            return "Boxer Bajarl";
                        case 39:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Magic Bajarl";
                            else
                                return "Magic Bajarl (N/S)";
                        case 40:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            return "Ultrarl";
                        default:
                            return "Undefined Monster";
                    }
                case 19: // Meowth, that's right!
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 1)
                                return "Mum Mew";
                            else
                                return "Mum Mew (N/S)";
                        case 11:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 1)
                                return "Bowwow";
                            else
                                return "Bowwow (N/S)";
                        case 13:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 1)
                                return "Eared Mew";
                            else
                                return "Eared Mew (N/S)";
                        case 19:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 1 && MonGR_Intelligence == 1 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 1)
                                return "Mew";
                            else
                                return "Mew (N/S)";
                        case 28:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Aqua Mew";
                            else
                                return "Aqua Mew (N/S)";
                        case 38:
                            return "Swimmer";
                        default:
                            return "Undefined Monster";
                    }
                case 20: // Ho-Oh
                    switch (MonGenus_Sub)
                    {
                        case 20:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 0 && MonGR_Intelligence == 4 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Phoenix";
                            else
                                return "Phoenix (N/S)";
                        case 38:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 0 && MonGR_Intelligence == 4 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Cinder Bird";
                            else
                                return "Cinder Bird (N/S)";
                        case 39:
                            return "[E] Blue Phoenix";
                        default:
                            return "Undefined Monster";
                    }
                case 21: // Casper the Spoopy Boi
                    switch (MonGenus_Sub)
                    {
                        case 21:
                            if (Mon_GutsRate == 7)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 0 && MonGR_Power == 0 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Ghost";
                            else
                                return "Ghost (N/S)";
                        case 38:
                            if (Mon_GutsRate == 7)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 0 && MonGR_Power == 0 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Chef";
                            else
                                return "Chef (N/S)";
                        default:
                            return "Undefined Monster";
                    }
                case 22: // YOU ARE A SCRUB.
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 6)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 4 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Love Seeker";
                            else
                                return "Love Seeker (N/S)";
                        case 22:
                            if (Mon_GutsRate == 6)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 0 && MonGR_Skill == 4 && MonGR_Speed == 0 && MonGR_Defence == 4)
                                return "Metalner";
                            else
                                return "Metalner (N/S)";
                        case 23:
                            if (Mon_GutsRate == 8)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 4 && MonGR_Speed == 0 && MonGR_Defence == 3)
                                return "Metazorl";
                            else
                                return "Metazorl (N/S)";
                        case 38:
                            return "Chinois";
                        default:
                            return "Undefined Monster";
                    }
                case 23: // Lexi's Ex's Favourite(!)
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Pink Eye";
                            else
                                return "Pink Eye (N/S)";
                        case 7:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Rocky Suezo";
                            else
                                return "Rocky Suezo (N/S)";
                        case 8:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Melon Suezo";
                            else
                                return "Melon Suezo (N/S)";
                        case 11:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Horn";
                            else
                                return "Horn (N/S)";
                        case 13:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Furred Suezo";
                            else
                                return "Furred Suezo (N/S)";
                        case 15:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Orion";
                            else
                                return "Orion (N/S)";
                        case 23:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Suezo";
                            else
                                return "Suezo (N/S)";
                        case 28:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Clear Suezo";
                            else
                                return "Clear Suezo (N/S)";
                        case 33:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Green Suezo";
                            else
                                return "Green Suezo (N/S)";
                        case 34:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Red Eye";
                            else
                                return "Red Eye (N/S)";
                        case 36:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Fly Eye";
                            else
                                return "Fly Eye (N/S)";
                        case 37:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Purple Suezo";
                            else
                                return "Purple Suezo (N/S)";
                        case 38:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Gold Suezo (DNA/Fusion)";
                            else
                                return "Gold Suezo";
                        case 39:
                            return "Silver Suezo";
                        case 40:
                            return "Bronze Suezo";
                        case 41:
                            return "Birdie";
                        case 42:
                            return "[E] White Suezo";
                        case 43:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 0 && MonGR_Power == 0 && MonGR_Intelligence == 0 && MonGR_Skill == 0 && MonGR_Speed == 0 && MonGR_Defence == 0 && Mon_InitLifespan == 1)
                                return "Sueki Suezo";
                            else
                                return "Sueki Suezo (N/S)";
                        default:
                            return "Undefined Monster";
                    }
                case 24: // Jills.
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Pong Pong";
                            else
                                return "Pong Pong (N/S)";
                        case 11:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Pierry";
                            else
                                return "Pierry (N/S)";
                        case 13:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Wondar";
                            else
                                return "Wondar (N/S)";
                        case 16:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 1 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Bengal";
                            else
                                return "Bengal (N/S)";
                        case 23:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Zorjil";
                            else
                                return "Zorjil (N/S)";
                        case 24:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 3 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Jill";
                            else
                                return "Jill (N/S)";
                        case 26:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Skull Capped";
                            else
                                return "Skull Capped (N/S)";
                        case 38:
                            return "Pithecan";
                        case 39:
                            return "[E] Bighand";
                        default:
                            return "Undefined Monster";
                    }
                case 25: // Vile Japanese Sweets
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 9)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 2)
                                return "Manna";
                            else
                                return "Manna (N/S)";
                        case 1:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 3)
                                return "Draco Mocchi";
                            else
                                return "Draco Mocchi (N/S)";
                        case 9:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 3)
                                return "Knight Mocchi";
                            else
                                return "Knight Mocchi (N/S)";
                        case 11:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 2)
                                return "Fake Penguin";
                            else
                                return "Fake Penguin (N/S)";
                        case 16:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 2)
                                return "Nyankoro";
                            else
                                return "Nyankoro (N/S)";
                        case 25:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 3)
                                return "Mocchi";
                            else
                                return "Mocchi (N/S)";
                        case 26:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Hell Pierrot";
                            else
                                return "Hell Pierrot (N/S)";
                        case 28:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 3)
                                return "Gelatine";
                            else
                                return "Gelatine (N/S)";
                        case 38:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 3)
                                return "Gentle-Mocchi (DNA Cap.)";
                            else
                                return "Gentle-Mocchi";
                        case 39:
                            return "Caloriena";
                        case 40:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 3)
                                return "White Mocchi (DNA Cap.)";
                            else
                                return "[E] White Mocchi";
                        case 41:
                            return "Mocchini";
                        default:
                            return "Undefined Monster";
                    }
                case 26: // HAIL SATAN
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 4 && MonGR_Skill == 4 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Hell Heart";
                            else
                                return "Hell Heart (N/S)";
                        case 1:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 4 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Flare Death";
                            else
                                return "Flare Death (N/S)";
                        case 7:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Tombstone";
                            else
                                return "Tombstone (N/S)";
                        case 11:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 4 && MonGR_Skill == 4 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Blue Terror";
                            else
                                return "Blue Terror (N/S)";
                        case 18:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 4 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Odium";
                            else
                                return "Oduim (N/S)";
                        case 26:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 4 && MonGR_Skill == 4 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Joker";
                            else
                                return "Joker (N/S)";
                        case 38:
                            return "Bloodshed";
                        default:
                            return "Undefined Monster";
                    }
                case 27: // arigatou okureiman
                    switch (MonGenus_Sub)
                    {
                        case 11:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Frozen Gaboo";
                            else
                                return "Frozen Gaboo (N/S)";
                        case 26:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Dokoo";
                            else
                                return "Dokoo (N/S)";
                        case 27:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 4 && MonGR_Power == 3 && MonGR_Intelligence == 0 && MonGR_Skill == 0 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Gaboo";
                            else
                                return "Gaboo (N/S)";
                        case 28:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 1 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Jelly Gaboo";
                            else
                                return "Jelly Gaboo (N/S)";
                        case 38:
                            return "Gaboo Soldier";
                        case 39:
                            return "[E] Mad Gaboo";
                        default:
                            return "Undefined Monster";
                    }
                case 28: // Jelly Tentacle Monsters
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Pink Jam";
                            else
                                return "Pink Jam (N/S)";
                        case 7:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Wall Mimic";
                            else
                                return "Wall Mimic (N/S)";
                        case 8:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Scaled Jell";
                            else
                                return "Scaled Jell (N/S)";
                        case 11:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Icy Jell";
                            else
                                return "Icy Jell (N/S)";
                        case 13:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Muddy Jell";
                            else
                                return "Muddy Jell (N/S)";
                        case 15:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Noble Jell";
                            else
                                return "Noble Jell (N/S)";
                        case 23:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Eye Jell";
                            else
                                return "Eye Jell (N/S)";
                        case 28:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Jell";
                            else
                                return "Jell (N/S)";
                        case 33:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Chloro Jell";
                            else
                                return "Chloro Jell (N/S)";
                        case 34:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Clay";
                            else
                                return "Clay (N/S)";
                        case 36:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Worm Jell";
                            else
                                return "Worm Jell (N/S)";
                        case 37:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Purple Jell";
                            else
                                return "Purple Jell (N/S)";
                        case 38:
                            return "Metal Jell";
                        default:
                            return "Undefined Monster";
                    }
                case 29: // Five guys, a couch, and Maria.
                    switch (MonGenus_Sub)
                    {
                        case 26:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 4 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Siren";
                            else
                                return "Siren (N/S)";
                        case 29:
                            if (Mon_GutsRate == 9)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 0 && MonGR_Intelligence == 3 && MonGR_Skill == 4 && MonGR_Speed == 3 && MonGR_Defence == 1)
                                return "Undine";
                            else
                                return "Undine (N/S)";
                        case 38:
                            return "Mermaid";
                        default:
                            return "Undefined Monster";
                    }
                case 30: // [K]Niton of Fire
                    switch (MonGenus_Sub)
                    {
                        case 7:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 4)
                                return "Ammon";
                            else
                                return "Ammon (N/S)";
                        case 9:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 4)
                                return "Knight Niton";
                            else
                                return "Knight Niton (N/S)";
                        case 16:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 2 && MonGR_Defence == 3)
                                return "Stripe Shell";
                            else
                                return "Stripe Shell (N/S)";
                        case 18:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Alabia Niton";
                            else
                                return "Alabia Niton (N/S)";
                        case 22:
                            if (Mon_GutsRate == 8)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 4)
                                return "Metal Shell";
                            else
                                return "Metal Shell (N/S)";
                        case 28:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 4)
                                return "Clear Shell";
                            else
                                return "Clear Shell (N/S)";
                        case 30:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 1 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 4)
                                return "Niton";
                            else
                                return "Niton (N/S)";
                        case 31:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Baum Kuchen";
                            else
                                return "Baum Kuchen (N/S)";
                        case 38:
                            return "Dribbler";
                        case 39:
                            return "Radial Niton";
                        case 40:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 1 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 4)
                                return "Disc Niton";
                            else
                                return "Disc Niton (N/S)";
                        default:
                            return "Undefined Monster";
                    }
                case 31: // Firewood
                    switch (MonGenus_Sub)
                    {
                        case 26:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 1 && MonGR_Intelligence == 4 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Ebony";
                            else
                                return "Ebony (N/S)";
                        case 31:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 0 && MonGR_Power == 1 && MonGR_Intelligence == 4 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Mock";
                            else
                                return "Mock (N/S)";
                        case 38:
                            return "White Birch";
                        case 39:
                            return "Pole Mock";
                        default:
                            return "Undefined Monster";
                    }
                case 32: // DON'T F**KEN WITH DUCKEN
                    switch (MonGenus_Sub)
                    {
                        case 7:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Blocken";
                            else
                                return "Blocken (N/S)";
                        case 23:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 3 && MonGR_Speed == 3 && MonGR_Defence == 0)
                                return "Ticken";
                            else
                                return "Ticken (N/S)";
                        case 32:
                            if (Mon_GutsRate == 8)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 4 && MonGR_Defence == 0)
                                return "Ducken";
                            else
                                return "Ducken (N/S)";
                        case 38:
                            return "Watermelony";
                        case 39:
                            return "Cawken";
                        default:
                            return "Undefined Monster";
                    }
                case 33: // Plants
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 8)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 0 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Queen Plant";
                            else
                                return "Queen Plant (N/S)";
                        case 7:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Rock Plant";
                            else
                                return "Rock Plant (N/S)";
                        case 8:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Scaled Plant";
                            else
                                return "Scaled Plant (N/S)";
                        case 11:
                            if (Mon_GutsRate == 8)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Blue Plant";
                            else
                                return "Blue Plant (N/S)";
                        case 13:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 0)
                                return "Hare Plant";
                            else
                                return "Hare Plant (N/S)";
                        case 15:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Gold Plant";
                            else
                                return "Gold Plant (N/S)";
                        case 23:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 1 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 0)
                                return "Usaba";
                            else
                                return "Usaba (N/S)";
                        case 28:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 0 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Mirage Plant";
                            else
                                return "Mirage Plant (N/S)";
                        case 33:
                            if (Mon_GutsRate == 8)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 4 && MonGR_Power == 0 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 0)
                                return "Plant";
                            else
                                return "Plant (N/S)";
                        case 34:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Black Plant";
                            else
                                return "Black Plant (N/S)";
                        case 36:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 4 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 0)
                                return "Fly Plant";
                            else
                                return "Fly Plant (N/S)";
                        case 37:
                            if (Mon_GutsRate == 9)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 1 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Weeds";
                            else
                                return "Weeds (N/S)";
                        case 38:
                            return "Raggae Plant";
                        default:
                            return "Undefined Monster";
                    }
                case 34: // Masters of Low Poly
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Romper Wall";
                            else
                                return "Romper Wall (N/S)";
                        case 7:
                            if (Mon_GutsRate == 18)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 4)
                                return "Obelisk";
                            else
                                return "Obelisk (N/S)";
                        case 8:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Jura Wall";
                            else
                                return "Jura Wall (N/S)";
                        case 11:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Blue Sponge";
                            else
                                return "Blue Sponge (N/S)";
                        case 13:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 1 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Furred Wall";
                            else
                                return "Furred Wall (N/S)";
                        case 15:
                            if (Mon_GutsRate == 18)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 3)
                                return "Ivory Wall";
                            else
                                return "Ivory Wall (N/S)";
                        case 23:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 3)
                                return "Sandy";
                            else
                                return "Sandy (N/S)";
                        case 28:
                            if (Mon_GutsRate == 16)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 4)
                                return "Ice Candy";
                            else
                                return "Ice Candy (N/S)";
                        case 33:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 2)
                                return "New Leaf";
                            else
                                return "New Leaf (N/S)";
                        case 34:
                            if (Mon_GutsRate == 18)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 4)
                                return "Monol";
                            else
                                return "Monol (N/S)";
                        case 36:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 3)
                                return "Soboros";
                            else
                                return "Soboros (N/S)";
                        case 37:
                            if (Mon_GutsRate == 15)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 3)
                                return "Asphaltum";
                            else
                                return "Asphaltum (N/S)";
                        case 38:
                            return "Galaxy";
                        case 39:
                            return "Dominos";
                        case 40:
                            if (Mon_GutsRate == 18)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 4)
                                return "Scribble";
                            else
                                return "Scribble (N/S)";
                        case 41:
                            return "[E] Burning Wall";
                        default:
                            return "Undefined Monster";
                    }
                case 35: // Monkey Magic
                    switch (MonGenus_Sub)
                    {
                        case 7:
                            if (Mon_GutsRate == 18)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 1 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Rock Ape";
                            else
                                return "Rock Ape (N/S)";
                        case 13:
                            if (Mon_GutsRate == 17)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 0 && MonGR_Skill == 2 && MonGR_Speed == 3 && MonGR_Defence == 2)
                                return "Gibberer";
                            else
                                return "Gibberer (N/S)";
                        case 15:
                            if (Mon_GutsRate == 18)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 3)
                                return "Bossy";
                            else
                                return "Bossy (N/S)";
                        case 33:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 2)
                                return "Tropical Ape";
                            else
                                return "Tropical Ape (N/S)";
                        case 35:
                            if (Mon_GutsRate == 18)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 0 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 3)
                                return "Ape";
                            else
                                return "Ape (N/S)";
                        case 38:
                            return "Gold Dust";
                        case 39:
                            return "[E] King Ape";
                        default:
                            return "Undefined Monster";
                    }
                case 36: // Wuuuurrrrmmms.
                    return MonWormGenus();
                case 37: // Nagas.
                    switch (MonGenus_Sub)
                    {
                        case 0:
                            if (Mon_GutsRate == 9)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 1 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Ripper";
                            else
                                return "Ripper (N/S)";
                        case 7:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Trident";
                            else
                                return "Trident (N/S)";
                        case 8:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 0 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Stinger";
                            else
                                return "Stinger (N/S)";
                        case 11:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Striker";
                            else
                                return "Striker (N/S)";
                        case 13:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 0 && MonGR_Skill == 3 && MonGR_Speed == 2 && MonGR_Defence == 1)
                                return "Edgehog"; // Shadow the Edgehog!
                            else
                                return "Edgehog (N/S)";
                        case 15:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Bazula";
                            else
                                return "Bazula (N/S)";
                        case 23:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Cyclops";
                            else
                                return "Cyclops (N/S)";
                        case 28:
                            if (Mon_GutsRate == 12)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Aqua Cutter";
                            else
                                return "Aqua Cutter (N/S)";
                        case 33:
                            if (Mon_GutsRate == 10)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 1)
                                return "Jungler";
                            else
                                return "Jungler (N/S)";
                        case 34:
                            if (Mon_GutsRate == 14)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 3)
                                return "Crimson Eyed";
                            else
                                return "Crimson Eyed (N/S)";
                        case 36:
                            if (Mon_GutsRate == 13)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Earth Keeper";
                            else
                                return "Earth Keeper (N/S)";
                        case 37:
                            if (Mon_GutsRate == 11)
                                MonGutsRateBox.BackColor = SystemColors.Control;
                            else
                                MonGutsRateBox.BackColor = Color.HotPink;

                            if (MonGR_LIF == 2 && MonGR_Power == 3 && MonGR_Intelligence == 0 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 2)
                                return "Naga";
                            else
                                return "Naga (N/S)";
                        case 38:
                            return "Time Noise";
                        case 39:
                            return "[E] Punisher";
                        default:
                            return "Undefined Monster";
                    }
                default: return "No Monster";
            }
        }

        public string MonWormGenus()
        {
            string WormOut = "Undefined Monster";
            bool bCocooned = false;

            switch (MonGenus_Main)
            {
                case 0:
                case 4:
                case 13:
                case 15:
                case 23:
                case 28:
                case 34:
                case 37:
                    if (MonGenus_Sub == 36 || (MonGenus_Main == 4 && MonGenus_Sub == 4 && (MonGR_LIF != 3 || MonGR_Power != 3 || MonGR_Intelligence != 0 || MonGR_Skill != 1 || MonGR_Speed != 1 || MonGR_Defence != 3)))
                        bCocooned = true;
                    break;
                default:
                    break;
            }

            if (bCocooned)
            {
                MonGutsRateBox.BackColor = SystemColors.Control;
                if (Mon_GutsRate == 12 && (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 1))
                    WormOut = "Cocooned Worm/Pixie";
                else if (Mon_GutsRate == 16 && (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 2))
                    WormOut = "Cocooned Worm/Golem";
                else if (Mon_GutsRate == 14 && (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 1))
                    WormOut = "Cocooned Worm/Zuum";
                else if (Mon_GutsRate == 13 && (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 1))
                    WormOut = "Cocooned Worm/Tiger";
                else if (Mon_GutsRate == 15 && (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 1))
                    WormOut = "Cocooned Worm/Hare";
                else if (Mon_GutsRate == 16 && (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 1))
                    WormOut = "Cocooned Worm/Gali";
                else if (Mon_GutsRate == 14 && (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 1))
                    WormOut = "Cocooned Worm/Suezo";
                else if (Mon_GutsRate == 15 && (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 2))
                    WormOut = "Cocooned Worm/Jell";
                else if (Mon_GutsRate == 12 && (MonGR_LIF == 4 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 1))
                    WormOut = "Cocooned Worm/Plant";
                else if (Mon_GutsRate == 16 && (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 2))
                    WormOut = "Cocooned Worm/Monol";
                else if (Mon_GutsRate == 15 && (MonGR_LIF == 4 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 1))
                    WormOut = "Cocooned Worm/Worm";
                else if (Mon_GutsRate == 13 && (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 1))
                    WormOut = "Cocooned Worm/Naga";
                else
                    MonGutsRateBox.BackColor = Color.HotPink;
            }
            else
            {
                switch (MonGenus_Sub)
                {
                    case 0:
                        if (Mon_GutsRate == 12)
                            MonGutsRateBox.BackColor = SystemColors.Control;
                        else
                            MonGutsRateBox.BackColor = Color.HotPink;

                        if (MonGR_LIF == 2 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 1)
                            WormOut = "Red Worm";
                        else
                            WormOut = "Red Worm (N/S)";
                        break;
                    case 7:
                        if (Mon_GutsRate == 16)
                            MonGutsRateBox.BackColor = SystemColors.Control;
                        else
                            MonGutsRateBox.BackColor = Color.HotPink;

                        if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 2 && MonGR_Skill == 1 && MonGR_Speed == 0 && MonGR_Defence == 2)
                            WormOut = "Rock Worm";
                        else
                            WormOut = "Rock Worm (N/S)";
                        break;
                    case 8:
                        if (Mon_GutsRate == 14)
                            MonGutsRateBox.BackColor = SystemColors.Control;
                        else
                            MonGutsRateBox.BackColor = Color.HotPink;

                        if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 1 && MonGR_Defence == 1)
                            WormOut = "Scaled Worm";
                        else
                            WormOut = "Scaled Worm (N/S)";
                        break;
                    case 11:
                        if (Mon_GutsRate == 13)
                            MonGutsRateBox.BackColor = SystemColors.Control;
                        else
                            MonGutsRateBox.BackColor = Color.HotPink;

                        if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 3 && MonGR_Speed == 1 && MonGR_Defence == 1)
                            WormOut = "Drill Tusk";
                        else
                            WormOut = "Drill Tusk (N/S)";
                        break;
                    case 13:
                        if (Mon_GutsRate == 15)
                            MonGutsRateBox.BackColor = SystemColors.Control;
                        else
                            MonGutsRateBox.BackColor = Color.HotPink;

                        if (MonGR_LIF == 3 && MonGR_Power == 3 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 2 && MonGR_Defence == 1)
                            WormOut = "Corone";
                        else
                            WormOut = "Corone (N/S)";
                        break;
                    case 15:
                        if (Mon_GutsRate == 16)
                            MonGutsRateBox.BackColor = SystemColors.Control;
                        else
                            MonGutsRateBox.BackColor = Color.HotPink;

                        if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 1)
                            WormOut = "Mask Worm";
                        else
                            WormOut = "Mask Worm (N/S)";
                        break;
                    case 23:
                        if (Mon_GutsRate == 14)
                            MonGutsRateBox.BackColor = SystemColors.Control;
                        else
                            MonGutsRateBox.BackColor = Color.HotPink;

                        if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 3 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 1)
                            WormOut = "Eye Worm";
                        else
                            WormOut = "Eye Worm (N/S)";
                        break;
                    case 28:
                        if (Mon_GutsRate == 15)
                            MonGutsRateBox.BackColor = SystemColors.Control;
                        else
                            MonGutsRateBox.BackColor = Color.HotPink;

                        if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 2)
                            WormOut = "Jelly Worm";
                        else
                            WormOut = "Jelly Worm (N/S)";
                        break;
                    case 33:
                        if (Mon_GutsRate == 12)
                            MonGutsRateBox.BackColor = SystemColors.Control;
                        else
                            MonGutsRateBox.BackColor = Color.HotPink;

                        if (MonGR_LIF == 4 && MonGR_Power == 1 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 1)
                            WormOut = "Flower Worm";
                        else
                            WormOut = "Flower Worm (N/S)";
                        break;
                    case 34:
                        if (Mon_GutsRate == 16)
                            MonGutsRateBox.BackColor = SystemColors.Control;
                        else
                            MonGutsRateBox.BackColor = Color.HotPink;

                        if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 2)
                            WormOut = "Black Worm";
                        else
                            WormOut = "Black Worm (N/S)";
                        break;
                    case 36:
                        if (Mon_GutsRate == 15)
                            MonGutsRateBox.BackColor = SystemColors.Control;
                        else
                            MonGutsRateBox.BackColor = Color.HotPink;

                        if (MonGR_LIF == 4 && MonGR_Power == 2 && MonGR_Intelligence == 2 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 1)
                            WormOut = "Worm";
                        else
                            WormOut = "Worm (N/S)";
                        break;
                    case 37:
                        if (Mon_GutsRate == 13)
                            MonGutsRateBox.BackColor = SystemColors.Control;
                        else
                            MonGutsRateBox.BackColor = Color.HotPink;

                        if (MonGR_LIF == 3 && MonGR_Power == 2 && MonGR_Intelligence == 1 && MonGR_Skill == 2 && MonGR_Speed == 0 && MonGR_Defence == 1)
                            WormOut = "Purple Worm";
                        else
                            WormOut = "Purple Worm (N/S)";
                        break;
                    case 38:
                        WormOut = "Express Worm";
                        break;
                    default:
                        WormOut = "Undefined Monster";
                        break;
                }
            }
            return WormOut;
        }
        public string MonMSAlphabetise(TextBox BoxID)
        {
            switch (Mon_ArenaSPD)
            {
                case 0: BoxID.BackColor = Color.OrangeRed; return "E";
                case 1: BoxID.BackColor = Color.Orange; return "D";
                case 2: BoxID.BackColor = Color.Yellow; return "C";
                case 3: BoxID.BackColor = Color.YellowGreen; return "B";
                case 4: BoxID.BackColor = Color.LimeGreen; return "A";
                default: BoxID.BackColor = SystemColors.Control; return "---";
            }
        }

        private string MonFormDescript()
        {
            if (Mon_Form >= -19 && Mon_Form <= 19) return "Normal";
            else if (Mon_Form >= 20 && Mon_Form <= 59) return "Fat";
            else if (Mon_Form >= 60 && Mon_Form <= 100) return "Plump";
            else if (Mon_Form >= -59 && Mon_Form <= -20) return "Slim";
            else if (Mon_Form >= -100 && Mon_Form <= -60) return "Skinny";
            else return "---";
        }

        public string MonGenusNames(int MonGene, bool bMainGenus)
        {
            switch (MonGene)
            {
                case 0: return "Pixie";
                case 1: return "Dragon";
                case 2: return "Centaur";
                case 3: return "C. Pandora";
                case 4: return "Beaclon";
                case 5: return "Henger";
                case 6: return "Wracky";
                case 7: return "Golem";
                case 8: return "Zuum";
                case 9: return "Durahan";
                case 10: return "Arrowhead";
                case 11: return "Tiger";
                case 12: return "Hopper";
                case 13: return "Hare";
                case 14: return "Baku";
                case 15: return "Gali";
                case 16: return "Kato";
                case 17: return "Zilla";
                case 18: return "Bajarl";
                case 19: return "Mew";
                case 20: return "Phoenix";
                case 21: return "Ghost";
                case 22: return "Metalner";
                case 23: return "Suezo";
                case 24: return "Jill";
                case 25: return "Mocchi";
                case 26: return "Joker";
                case 27: return "Gaboo";
                case 28: return "Jell";
                case 29: return "Undine";
                case 30: return "Niton";
                case 31: return "Mock";
                case 32: return "Ducken";
                case 33: return "Plant";
                case 34: return "Monol";
                case 35: return "Ape";
                case 36: return "Worm";
                case 37: return "Naga";
                case 38: if (!bMainGenus) return "??? #1"; else return "---";
                case 39: if (!bMainGenus) return "??? #2"; else return "---";
                case 40: if (!bMainGenus) return "??? #3"; else return "---";
                case 41: if (!bMainGenus) return "??? #4"; else return "---";
                case 42: if (!bMainGenus) return "??? #5"; else return "---";
                case 43: if (!bMainGenus) return "??? #6"; else return "---";
                default: return "---";
            }
        }

        public string MonDesireNames(int MonItem)
        {
            switch (MonItem)
            {
                case 0: return "Potato";
                case 1: return "Fish";
                case 2: return "Meat";
                case 3: return "Milk";
                case 4: return "Cup Jelly";
                case 5: return "Tablet";
                case 6: return "Sculpture";
                case 7: return "Gemini's Pot";
                case 8: return "Lump of Ice";
                case 9: return "Fire Stone";
                case 10: return "Pure Gold";
                case 11: return "Pure Gold"; // Gold Suezo Special
                case 12: return "Pure Silver";
                case 13: return "Pure Platina";
                case 14: return "Mango";
                case 15: return "Candy";
                case 16: return "Smoked Snake";
                case 17: return "Apple Cake";
                case 18: return "Mint Leaf";
                case 19: return "Powder";
                case 20: return "Sweet Jelly";
                case 21: return "Sour Jelly";
                case 22: return "Crab's Claw";
                case 23: return "Nuts Oil";
                case 24: return "Nuts Oil (HAD)";
                case 25: return "Star Prune";
                case 26: return "Gold Peach";
                case 27: return "Silver Peach";
                case 28:
                case 29:
                case 30: return "Magic Banana";
                case 31: return "Half-eaten";
                case 32: return "Irritater";
                case 33: return "Griever";
                case 34: return "Teromeann";
                case 35: return "Manseitan";
                case 36: return "Larox";
                case 37: return "Kasseitan";
                case 38: return "Troron";
                case 39: return "Nageel";
                case 40: return "Torokachin FX";
                case 41: return "Paradoxine";
                case 42:
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48:
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57: return "Disc Chips";
                case 58: return "Bay Shrimp";
                case 59: return "Incense";
                case 60: return "Shoes";
                case 61: return "Rice Cracker";
                case 62: return "Tobacco";
                case 63: return "Olive Oil";
                case 64: return "Kaleidoscope";
                case 65: return "Torles Water";
                case 66: return "Broken Pipe";
                case 67: return "Perfume";
                case 68: return "Stick (Red)";
                case 69: return "Bone";
                case 70: return "Perfume Oil";
                case 71: return "Wool Ball";
                case 72: return "Cedar Log";
                case 73: return "Pile of Meat";
                case 74: return "Soil";
                case 75: return "Rock Candy";
                case 76: return "Training Dummy";
                case 77: return "Ice of Papas";
                case 78: return "Grease";
                case 79:
                case 80:
                case 81:
                case 82:
                case 83:
                case 84:
                case 85: return "DNA Capsule";
                case 86: return "Gods' Slate";
                case 87: return "Hero Badge";
                case 88: return "Heel Badge";
                case 89: return "Quack Doll";
                case 90: return "Dragon Tusk";
                case 91: return "Old Sheath";
                case 92: return "Double-edged";
                case 93: return "Magic Pot";
                case 94: return "Mask";
                case 95: return "Big Footstep";
                case 96: return "Big Boots";
                case 97: return "Fire Feather";
                case 98: return "Taurus' Horn";
                case 99: return "Dino's Tail";
                case 100: return "Zilla Beard";
                case 101: return "Fun Can";
                case 102: return "Strong Glue";
                case 103: return "Quack Doll";
                case 104: return "Mystic Seed";
                case 105: return "Parepare Tea";
                case 106: return "Match";
                case 107: return "Tooth Pick";
                case 108: return "Playmate";
                case 109: return "Whet Stone";
                case 110: return "Polish";
                case 111: return "Silk Cloth";
                case 112: return "Disc Dish";
                case 113: return "Gramophone";
                case 114: return "Shiny Stone";
                case 115: return "Meteorite";
                case 116: return "Steamed Bun";
                case 117: return "Razor Blade";
                case 118: return "Ice Candy";
                case 119: return "Fish Bone";
                case 120: return "Sun Lamp";
                case 121: return "Silk Hat";
                case 122: return "Half Cake";
                case 123: return "Shaved Ice";
                case 124: return "Sweet Potato";
                case 125: return "Medal";
                case 126: return "Gold Medal";
                case 127: return "Silver Medal";
                case 128: return "Music Box";
                case 129: return "Medallion";
                case 130: return "Nothing";
                case 131: return "Battle";
                case 132: return "Rest";
                case 133: return "Play";
                case 134: return "Mirror";
                case 135: return "Colart Tea";
                case 136: return "Galoe Nut";
                case 137: return "Stick of Ice";
                case 138: return "Ocean Stone";
                case 139: return "Seaweed";
                case 140:
                case 141:
                case 142: return "Clay Doll";
                case 143: return "Mock Nut";
                case 144: return "Colt's Cake";
                case 145: return "Flower";
                case 146:
                case 147:
                case 148:
                case 149:
                case 150:
                case 151:
                case 152:
                case 153:
                case 154:
                case 155:
                case 156:
                case 157:
                case 158:
                case 159:
                case 160:
                case 161:
                case 162:
                case 163:
                case 164:
                case 165:
                case 166:
                case 167: return "Disc Chips";
                case 168: return "Gali Mask";
                case 169: return "Crystal";
                case 170: return "Undine Slate";
                case 171:
                case 172: return "Money";
                case 173: return "Stick (Green)";
                case 174: return "Cup Jelly (D)";
                case 175: return "Spear";
                case 176: return "Wracky Doll";
                case 177: return "Quack Doll";
                default: return "---";
            }
        }

        [Flags]
        private enum BattleSpecials
        {
            None = 0,
            Power = 1,
            Anger = 2,
            Grit = 4,
            Will = 8,
            Fight = 16,
            Fury = 32,
            Guard = 64,
            Ease = 128,
            Hurry = 256,
            Vigor = 512,
            Real = 1024,
            Drunk = 2048,
            Unity = 4096,
            HAX1 = 8192,
            HAX2 = 16384,
            HAX3 = 32768
        }

        [Flags]
        private enum CInput
        {
            None = 0,
            L2 = 1,
            R2 = 2,
            L1 = 4,
            R1 = 8,
            Circle = 16,
            Cross = 32,
            Triangle = 64,
            Square = 128,
            Select = 256,
            L3 = 512,
            R3 = 1024,
            Start = 2048,
            Up = 4096,
            Right = 8192,
            Down = 16384,
            Left = 32768
        }

        private string MonAgeDisplay()
        {
            if (Mon_Age == -1)
                return "---";
            if (AgeWeeksOnly)
                return Mon_Age + "w";

            int Mon_AgeWeeks = Mon_Age % 4;
            int Mon_AgeMonths = (Mon_Age % 48) / 4;
            int Mon_AgeYears = Mon_Age / 48;
            return Mon_AgeYears + "y," + Mon_AgeMonths + "m," + Mon_AgeWeeks + "w";
        }

        public string MonPlaytimeNames(int MonPlay)
        {
            switch (MonPlay)
            {
                case 0: return "Mud Fight";
                case 1: return "Sumo Battle";
                case 2: return "Sparring";
                default: return "---";
            }
        }

        public string MonLifeStageNames(int MonLife)
        {
            if (Mon_Lifespan <= 0 && MonBreedNameBox.Text != "No Monster"/* || [some trigger for death by battle]*/)
                return "11 - Dead";
            switch (MonLife)
            {
                case 0: return "1 - Baby";
                case 1: return "2 - Child";
                case 2: return "3 - Adolescent";
                case 3: return "4 - Adolescent 2";
                case 4: return "5 - Prime";
                case 5: return "6 - Sub-Prime";
                case 6: return "7 - Elder";
                case 7: return "8 - Elder 2";
                case 8: return "9 - Old Age";
                case 9: return "10 - Twilight";
                default: return "---";
            }
        }

        private string MonNature_Display()
        {
            double tmp_angle;
            int nature_mod;
            double nature_radian;

            nature_radian = Math.PI * Mon_Nature / 2048;

            tmp_angle = Math.Sin(nature_radian);
            tmp_angle *= 100;
            Math.Truncate(tmp_angle);

            nature_mod = (int)tmp_angle;
            Mon_EffNature = Mon_NatureBase + nature_mod;

            if (RawNatureMod)
                return Mon_Nature.ToString();

            if (Mon_EffNature < 20 && Mon_EffNature > -20)
            {
                MonNatureModBox.BackColor = SystemColors.Control;
                MonNatureModBox.ForeColor = SystemColors.ControlText;
                return Mon_EffNature + " (Neutral)";
            }
            else if (Mon_EffNature <= -20 && Mon_EffNature > -60)
            {
                MonNatureModBox.BackColor = Color.DarkGray;
                MonNatureModBox.ForeColor = Color.White;
                return Mon_EffNature + " (Bad)";
            }
            else if (Mon_EffNature <= -60)
            {
                MonNatureModBox.BackColor = Color.Black;
                MonNatureModBox.ForeColor = Color.White;
                return Mon_EffNature + " (Worst)";
            }
            else if (Mon_EffNature < 60 && Mon_EffNature >= 20)
            {
                MonNatureModBox.BackColor = Color.LightYellow;
                MonNatureModBox.ForeColor = SystemColors.ControlText;
                return Mon_EffNature + " (Good)";
            }
            else if (Mon_EffNature >= 60)
            {
                MonNatureModBox.BackColor = Color.White;
                MonNatureModBox.ForeColor = SystemColors.ControlText;
                return Mon_EffNature + " (Best)";
            }
            else
            {
                MonNatureModBox.BackColor = Color.HotPink;
                MonNatureModBox.ForeColor = SystemColors.ControlText;
                return Mon_EffNature + " (???)";
            }
        }

        private bool MR2ReadBool(int Addr)
        {
            ReadProcessMemory(psxPTR, PSXBase + Addr, ScratchData, 1, ref HasRead);
            return Convert.ToBoolean(ScratchData[0]);
        }

        private int MR2ReadInt(int Addr)
        {
            ReadProcessMemory(psxPTR, PSXBase + Addr, ScratchData, 1, ref HasRead);
            return Convert.ToInt16(ScratchData[0]);
        }

        private int MR2ReadDouble(int Addr)
        {
            ReadProcessMemory(psxPTR, PSXBase + Addr, ScratchData, 2, ref HasRead);
            return BitConverter.ToInt16(ScratchData, 0);
        }

        private int MR2ReadQuad(int Addr)
        {
            ReadProcessMemory(psxPTR, PSXBase + Addr, ScratchData, 4, ref HasRead);
            return BitConverter.ToInt32(ScratchData, 0);
        }

        private string MonReadMoney()
        {
            int PlayerMoney;
            Array.Clear(ScratchData, 0, 4);

            if (EmuVer != DXSelectionID)
            {
                if (MR2Mode.SelectedIndex < 2)
                    // Monster Rancher 2
                    ReadProcessMemory(psxPTR, PSXBase + 0x00098FBC, ScratchData, 4, ref HasRead);
                else if (MR2Mode.SelectedIndex == 2)
                    ReadProcessMemory(psxPTR, PSXBase + 0x00096F6C, ScratchData, 4, ref HasRead);
            }
            else
            {
                ReadProcessMemory(psxPTR, PSXBase + 0x0009A568, ScratchData, 4, ref HasRead);
            }
            PlayerMoney = BitConverter.ToInt32(ScratchData, 0);
            return PlayerMoney.ToString() + "G";
        }

        private string MonReadPrizeMoney()
        {
            int MonsterMoney;
            Array.Clear(ScratchData, 0, 4);

            if (EmuVer != DXSelectionID)
            {
                if (MR2Mode.SelectedIndex < 2)
                    // Monster Rancher 2
                    ReadProcessMemory(psxPTR, PSXBase + 0x00097A5C, ScratchData, 4, ref HasRead); // TODO: Find Prize Money for MR2
                else if (MR2Mode.SelectedIndex == 2)
                    ReadProcessMemory(psxPTR, PSXBase + 0x00095A0C, ScratchData, 4, ref HasRead); // TODO: Find Prize Money for MF2
            }
            else
            {
                ReadProcessMemory(psxPTR, PSXBase + 0x00097A54, ScratchData, 4, ref HasRead);
            }
            MonsterMoney = BitConverter.ToInt32(ScratchData, 0);
            return MonsterMoney.ToString() + "G";
        }

        private string MonReadGivenName()
        {
            string MonGivenName = "";
            int CharaID;

            if (EmuVer == DXSelectionID)
            {
                for (int i = 0; i < 24; i++)                                                                                                            //24 bytes long //bedeg
                {
                    ReadProcessMemory(psxPTR, PSXBase + 0x0097B78 + i/* * 2*/, ScratchData, 1/*2*/, ref HasRead);
                    CharaID = (ScratchData[0]);                                                                                                         //read 1 byte first
                    if (CharaID == 0xb0 | CharaID == 0xb1 | CharaID == 0xb2 | CharaID == 0xb3 | CharaID == 0xb4 | CharaID == 0xb5 | CharaID == 0xb6)    //if it's any of these read 2 bytes
                    {
                        ReadProcessMemory(psxPTR, PSXBase + 0x0097B78 + i, ScratchData, 2, ref HasRead);
                        CharaID = ((ScratchData[0] << 8) + ScratchData[1]);
                        if (CharaID == 0xFFFF)
                            break;

                        if (CharMapping.charMap.ContainsKey(CharaID))
                            MonGivenName += CharMapping.charMap[CharaID];
                        else
                            MonGivenName += "?";                                                                                                        //display "?" if character is unrecognized

                        i++;
                    }
                    else                                                                                                                                //read 1 byte for number chars and possibly space if CE'd
                    {
                        if (CharaID == 0xFF)
                            break;

                        if (CharMapping.charMap.ContainsKey(CharaID))
                            MonGivenName += CharMapping.charMap[CharaID];
                        else
                            MonGivenName += "?";                                                                                                        //display "?" if character is unrecognized
                    }
                }

                if (MonGivenName != "" && !bChangingName)
                    ChangeName.Visible = true;
                else
                    ChangeName.Visible = false;
            }
            else
            {
                if (MR2Mode.SelectedIndex < 2)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        ReadProcessMemory(psxPTR, PSXBase + 0x00097B78 + i, ScratchData, 1, ref HasRead);
                        if (ScratchData[0] == 255)
                        {
                            break;
                        }
                        MonGivenName += ExportEnglishPS1(ScratchData[0], false);
                    }
                }
                else if (MR2Mode.SelectedIndex == 2)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        ReadProcessMemory(psxPTR, PSXBase + 0x00095B28 + i, ScratchData, 1, ref HasRead);
                        CharaID = ScratchData[0];

                        switch (CharaID)
                        {
                            case 0: MonGivenName += " "; break; // space
                                // Hiragana START
                            case 1: MonGivenName += "あ"; break;
                            case 2: MonGivenName += "い"; break;
                            case 3: MonGivenName += "う"; break;
                            case 4: MonGivenName += "え"; break;
                            case 5: MonGivenName += "お"; break;
                            case 6: MonGivenName += "か"; break;
                            case 7: MonGivenName += "き"; break;
                            case 8: MonGivenName += "く"; break;
                            case 9: MonGivenName += "け"; break;
                            case 10: MonGivenName += "こ"; break;
                            case 11: MonGivenName += "さ"; break;
                            case 12: MonGivenName += "し"; break;
                            case 13: MonGivenName += "す"; break;
                            case 14: MonGivenName += "せ"; break;
                            case 15: MonGivenName += "そ"; break;
                            case 16: MonGivenName += "た"; break;
                            case 17: MonGivenName += "ち"; break;
                            case 18: MonGivenName += "つ"; break;
                            case 19: MonGivenName += "て"; break;
                            case 20: MonGivenName += "と"; break;
                            case 21: MonGivenName += "な"; break;
                            case 22: MonGivenName += "に"; break;
                            case 23: MonGivenName += "ぬ"; break;
                            case 24: MonGivenName += "ね"; break;
                            case 25: MonGivenName += "の"; break;
                            case 26: MonGivenName += "は"; break;
                            case 27: MonGivenName += "ひ"; break;
                            case 28: MonGivenName += "ふ"; break;
                            case 29: MonGivenName += "へ"; break;
                            case 30: MonGivenName += "ほ"; break;
                            case 31: MonGivenName += "ま"; break;
                            case 32: MonGivenName += "み"; break;
                            case 33: MonGivenName += "む"; break;
                            case 34: MonGivenName += "め"; break;
                            case 35: MonGivenName += "も"; break;
                            case 36: MonGivenName += "や"; break;
                            case 37: MonGivenName += "ゆ"; break;
                            case 38: MonGivenName += "よ"; break;
                            case 39: MonGivenName += "ら"; break;
                            case 40: MonGivenName += "り"; break;
                            case 41: MonGivenName += "る"; break;
                            case 42: MonGivenName += "れ"; break;
                            case 43: MonGivenName += "ろ"; break;
                            case 44: MonGivenName += "わ"; break;
                            case 45: MonGivenName += "を"; break;
                            case 46: MonGivenName += "ん"; break;
                            case 47: MonGivenName += "が"; break;
                            case 48: MonGivenName += "ぎ"; break;
                            case 49: MonGivenName += "ぐ"; break;
                            case 50: MonGivenName += "げ"; break;
                            case 51: MonGivenName += "ご"; break;
                            case 52: MonGivenName += "ざ"; break;
                            case 53: MonGivenName += "じ"; break;
                            case 54: MonGivenName += "ず"; break;
                            case 55: MonGivenName += "ぜ"; break;
                            case 56: MonGivenName += "ぞ"; break;
                            case 57: MonGivenName += "だ"; break;
                            case 58: MonGivenName += "ぢ"; break;
                            case 59: MonGivenName += "づ"; break;
                            case 60: MonGivenName += "で"; break;
                            case 61: MonGivenName += "ど"; break;
                            case 62: MonGivenName += "ば"; break;
                            case 63: MonGivenName += "び"; break;
                            case 64: MonGivenName += "ぶ"; break;
                            case 65: MonGivenName += "べ"; break;
                            case 66: MonGivenName += "ぼ"; break;
                            case 67: MonGivenName += "ぁ"; break;
                            case 68: MonGivenName += "ぃ"; break;
                            case 69: MonGivenName += "ぅ"; break;
                            case 70: MonGivenName += "ぇ"; break;
                            case 71: MonGivenName += "ぉ"; break;
                            case 72: MonGivenName += "ゃ"; break;
                            case 73: MonGivenName += "ゅ"; break;
                            case 74: MonGivenName += "ょ"; break;
                            case 75: MonGivenName += "ぱ"; break;
                            case 76: MonGivenName += "ぴ"; break;
                            case 77: MonGivenName += "ぷ"; break;
                            case 78: MonGivenName += "ぺ"; break;
                            case 79: MonGivenName += "ぽ"; break;
                            case 80: MonGivenName += "っ"; break;
                                // Katakana START - Hiragana END
                            case 81: MonGivenName += "ア"; break;
                            case 82: MonGivenName += "イ"; break;
                            case 83: MonGivenName += "ウ"; break;
                            case 84: MonGivenName += "エ"; break;
                            case 85: MonGivenName += "オ"; break;
                            case 86: MonGivenName += "カ"; break;
                            case 87: MonGivenName += "キ"; break;
                            case 88: MonGivenName += "ク"; break;
                            case 89: MonGivenName += "ケ"; break;
                            case 90: MonGivenName += "コ"; break;
                            case 91: MonGivenName += "サ"; break;
                            case 92: MonGivenName += "シ"; break;
                            case 93: MonGivenName += "ス"; break;
                            case 94: MonGivenName += "セ"; break;
                            case 95: MonGivenName += "ソ"; break;
                            case 96: MonGivenName += "た"; break;
                            case 97: MonGivenName += "チ"; break;
                            case 98: MonGivenName += "ツ"; break;
                            case 99: MonGivenName += "テ"; break;
                            case 100: MonGivenName += "ト"; break;
                            case 101: MonGivenName += "ナ"; break;
                            case 102: MonGivenName += "ニ"; break;
                            case 103: MonGivenName += "ヌ"; break;
                            case 104: MonGivenName += "ネ"; break;
                            case 105: MonGivenName += "ノ"; break;
                            case 106: MonGivenName += "ハ"; break;
                            case 107: MonGivenName += "ヒ"; break;
                            case 108: MonGivenName += "フ"; break;
                            case 109: MonGivenName += "ヘ"; break;
                            case 110: MonGivenName += "ホ"; break;
                            case 111: MonGivenName += "マ"; break;
                            case 112: MonGivenName += "ミ"; break;
                            case 113: MonGivenName += "ム"; break;
                            case 114: MonGivenName += "メ"; break;
                            case 115: MonGivenName += "モ"; break;
                            case 116: MonGivenName += "ヤ"; break;
                            case 117: MonGivenName += "ユ"; break;
                            case 118: MonGivenName += "ヨ"; break;
                            case 119: MonGivenName += "ラ"; break;
                            case 120: MonGivenName += "リ"; break;
                            case 121: MonGivenName += "ル"; break;
                            case 122: MonGivenName += "レ"; break;
                            case 123: MonGivenName += "ロ"; break;
                            case 124: MonGivenName += "わ"; break;
                            case 125: MonGivenName += "ヲ"; break;
                            case 126: MonGivenName += "ン"; break;
                            case 127: MonGivenName += "ガ"; break;
                            case 128: MonGivenName += "ギ"; break;
                            case 129: MonGivenName += "グ"; break;
                            case 130: MonGivenName += "ゲ"; break;
                            case 131: MonGivenName += "ゴ"; break;
                            case 132: MonGivenName += "ザ"; break;
                            case 133: MonGivenName += "ジ"; break;
                            case 134: MonGivenName += "ズ"; break;
                            case 135: MonGivenName += "ゼ"; break;
                            case 136: MonGivenName += "ゾ"; break;
                            case 137: MonGivenName += "ダ"; break;
                            case 138: MonGivenName += "ヂ"; break;
                            case 139: MonGivenName += "ヅ"; break;
                            case 140: MonGivenName += "デ"; break;
                            case 141: MonGivenName += "ド"; break;
                            case 142: MonGivenName += "バ"; break;
                            case 143: MonGivenName += "ビ"; break;
                            case 144: MonGivenName += "ブ"; break;
                            case 145: MonGivenName += "ベ"; break;
                            case 146: MonGivenName += "ボ"; break;
                            case 147: MonGivenName += "ァ"; break;
                            case 148: MonGivenName += "ィ"; break;
                            case 149: MonGivenName += "ゥ"; break;
                            case 150: MonGivenName += "ェ"; break;
                            case 151: MonGivenName += "ォ"; break;
                            case 152: MonGivenName += "ャ"; break;
                            case 153: MonGivenName += "ュ"; break;
                            case 154: MonGivenName += "ョ"; break;
                            case 155: MonGivenName += "パ"; break;
                            case 156: MonGivenName += "ピ"; break;
                            case 157: MonGivenName += "プ"; break;
                            case 158: MonGivenName += "ペ"; break;
                            case 159: MonGivenName += "ポ"; break;
                            case 160: MonGivenName += "ッ"; break;
                            case 161: MonGivenName += "ヴ"; break;
                            case 162: MonGivenName += "0"; break;
                            case 163: MonGivenName += "1"; break;
                            case 164: MonGivenName += "2"; break;
                            case 165: MonGivenName += "3"; break;
                            case 166: MonGivenName += "4"; break;
                            case 167: MonGivenName += "5"; break;
                            case 168: MonGivenName += "6"; break;
                            case 169: MonGivenName += "7"; break;
                            case 170: MonGivenName += "8"; break;
                            case 171: MonGivenName += "9"; break;
                            case 176: i++; ReadProcessMemory(psxPTR, PSXBase + 0x00095B28 + i, ScratchData, 1, ref HasRead); MonGivenName += ExportEnglishPS1(ScratchData[0], true); break; // English Letters.
                            case 255: i = 15; break; // Null Terminate
                            default: MonGivenName += "_"; break;
                        }
                    }
                }
            }
            return MonGivenName;
        }

        private string ExportEnglishPS1(int characterID, bool bJapaneseMode)
        {
            if (bJapaneseMode)
                characterID += 11;

            if (characterID > 92)
                characterID = 92;

            switch (characterID)
            {
                case 0: return " ";
                case 1: return "0";
                case 2: return "1";
                case 3: return "2";
                case 4: return "3";
                case 5: return "4";
                case 6: return "5";
                case 7: return "6";
                case 8: return "7";
                case 9: return "8";
                case 10: return "9";
                case 11: return "A";
                case 12: return "B";
                case 13: return "C";
                case 14: return "D";
                case 15: return "E";
                case 16: return "F";
                case 17: return "G";
                case 18: return "H";
                case 19: return "I";
                case 20: return "J";
                case 21: return "K";
                case 22: return "L";
                case 23: return "M";
                case 24: return "N";
                case 25: return "O";
                case 26: return "P";
                case 27: return "Q";
                case 28: return "R";
                case 29: return "S";
                case 30: return "T";
                case 31: return "U";
                case 32: return "V";
                case 33: return "W";
                case 34: return "X";
                case 35: return "Y";
                case 36: return "Z";
                case 37: return "a";
                case 38: return "b";
                case 39: return "c";
                case 40: return "d";
                case 41: return "e";
                case 42: return "f";
                case 43: return "g";
                case 44: return "h";
                case 45: return "i";
                case 46: return "j";
                case 47: return "k";
                case 48: return "l";
                case 49: return "m";
                case 50: return "n";
                case 51: return "o";
                case 52: return "p";
                case 53: return "q";
                case 54: return "r";
                case 55: return "s";
                case 56: return "t";
                case 57: return "u";
                case 58: return "v";
                case 59: return "w";
                case 60: return "x";
                case 61: return "y";
                case 62: return "z";
                case 63: return ".";
                case 64: return "·";
                case 65: return "!";
                case 66: return "?";
                case 67: return "-";
                case 68: return ",";
                case 69: return "。";
                case 70: return "+";
                case 71: return "x";
                case 72: return "/";
                case 73: return "-";
                case 74: return "%";
                case 75: return "=";
                case 76: return "「";
                case 77: return "」";
                case 78: return "O";
                case 79: return "∆";
                case 80: return "□";
                case 81: return "X";
                case 84: return "`";
                case 85: return "\"";
                case 86: return ":";
                case 87: return "~";
                case 90: return "(";
                case 91: return ")";
                case 92: return "_";
                default:
                    return "_";
            }
        }

        private void ReadInput()
        {
            if (cv == null)
                return;

            ReadProcessMemory(psxPTR, PSXBase + 0x000938AE, ScratchData, 2, ref HasRead);
            P1Input = BitConverter.ToInt16(ScratchData, 0);
            CInput P1 = (CInput)P1Input;
            Con1Input[0] = (P1 & CInput.L2) == CInput.L2;
            Con1Input[1] = (P1 & CInput.R2) == CInput.R2;
            Con1Input[2] = (P1 & CInput.L1) == CInput.L1;
            Con1Input[3] = (P1 & CInput.R1) == CInput.R1;
            Con1Input[4] = (P1 & CInput.Circle) == CInput.Circle;
            Con1Input[5] = (P1 & CInput.Cross) == CInput.Cross;
            Con1Input[6] = (P1 & CInput.Triangle) == CInput.Triangle;
            Con1Input[7] = (P1 & CInput.Square) == CInput.Square;
            Con1Input[8] = (P1 & CInput.Select) == CInput.Select;
            Con1Input[9] = (P1 & CInput.L3) == CInput.L3;
            Con1Input[10] = (P1 & CInput.R3) == CInput.R3;
            Con1Input[11] = (P1 & CInput.Start) == CInput.Start;
            Con1Input[12] = (P1 & CInput.Up) == CInput.Up;
            Con1Input[13] = (P1 & CInput.Right) == CInput.Right;
            Con1Input[14] = (P1 & CInput.Down) == CInput.Down;
            Con1Input[15] = (P1 & CInput.Left) == CInput.Left;
        }

        private string MonReadBattleSpecials()
        {
            if (EmuVer != DXSelectionID) // If this is PS1 emulation
            {
                if(MR2Mode.SelectedIndex < 2)
                    ReadProcessMemory(psxPTR, PSXBase + 0x00097BD8, ScratchData, 2, ref HasRead);
                else if (MR2Mode.SelectedIndex == 2)
                    ReadProcessMemory(psxPTR, PSXBase + 0x00095B88, ScratchData, 2, ref HasRead);
            }
            else // If this is MR2DX
            {
                ReadProcessMemory(psxPTR, PSXBase + 0x00097BE0, ScratchData, 2, ref HasRead);
            }
            int SpecialsInt = BitConverter.ToInt16(ScratchData, 0);
            bool bInvalid = false;
            BattleSpecials BSFL = (BattleSpecials)SpecialsInt;

            string specialList = "";

            if ((BSFL & BattleSpecials.Power) == BattleSpecials.Power)
            {
                if (Mon_EffNature >= 20)
                    specialList += "[Power]";
                else
                    specialList += "Power";
            }
            if ((BSFL & BattleSpecials.Anger) == BattleSpecials.Anger)
            {
                if (specialList.Length > 0)
                    specialList += ", ";

                if (Mon_EffNature <= -20)
                    specialList += "[Anger]";
                else
                    specialList += "Anger";
            }
            if ((BSFL & BattleSpecials.Grit) == BattleSpecials.Grit)
            {
                if (specialList.Length > 0)
                    specialList += ", ";
                specialList += "Grit";
            }
            if ((BSFL & BattleSpecials.Will) == BattleSpecials.Will)
            {
                if (specialList.Length > 0)
                    specialList += ", ";
                specialList += "Will";
            }
            if ((BSFL & BattleSpecials.Fight) == BattleSpecials.Fight)
            {
                if (specialList.Length > 0)
                    specialList += ", ";
                specialList += "Fight";
            }
            if ((BSFL & BattleSpecials.Fury) == BattleSpecials.Fury)
            {
                if (specialList.Length > 0)
                    specialList += ", ";
                specialList += "Fury";
            }
            if ((BSFL & BattleSpecials.Guard) == BattleSpecials.Guard)
            {
                if (specialList.Length > 0)
                    specialList += ", ";
                specialList += "Guard";
            }
            if ((BSFL & BattleSpecials.Ease) == BattleSpecials.Ease)
            {
                if (specialList.Length > 0)
                    specialList += ", ";
                specialList += "Ease";
            }
            if ((BSFL & BattleSpecials.Hurry) == BattleSpecials.Hurry)
            {
                if (specialList.Length > 0)
                    specialList += ", ";
                specialList += "Hurry";
            }
            if ((BSFL & BattleSpecials.Vigor) == BattleSpecials.Vigor)
            {
                if (specialList.Length > 0)
                    specialList += ", ";
                specialList += "Vigor";
            }
            if ((BSFL & BattleSpecials.Real) == BattleSpecials.Real)
            {
                if (specialList.Length > 0)
                    specialList += ", ";
                specialList += "Real";

                if (MonGenus_Main != 26 && MonGenus_Sub != 26)
                    bInvalid = true;
            }
            if ((BSFL & BattleSpecials.Drunk) == BattleSpecials.Drunk)
            {
                if (specialList.Length > 0)
                    specialList += ", ";
                specialList += "Drunk";

                if (MonGenus_Main != 16)
                    bInvalid = true;
            }
            if ((BSFL & BattleSpecials.Unity) == BattleSpecials.Unity)
            {
                if (specialList.Length > 0)
                    specialList += ", ";
                specialList += "Unity";

                if (MonGenus_Main != 3)
                    bInvalid = true;
            }
            if (((BSFL & BattleSpecials.HAX1) == BattleSpecials.HAX1) || ((BSFL & BattleSpecials.HAX2) == BattleSpecials.HAX2) || ((BSFL & BattleSpecials.HAX3) == BattleSpecials.HAX3))
            {
                bInvalid = true;
                if (specialList.Length > 0)
                    specialList += ", ";
                specialList += "Unused Special";
            }
            if (bInvalid)
                MonSpecialsBox.BackColor = Color.HotPink;
            else
                MonSpecialsBox.BackColor = SystemColors.Control;
            return specialList;
        }

        private bool GrabPSXProcess()
        {
            PSXProcess = null;
            switch (EmuVer)
            {
                case 0:
                    EmuFileName = "ePSXe"; break;
                case 1:
                    EmuFileName = "psxfin"; break;
                case 2:
                    EmuFileName = "XEBRA"; break;
                case 3:
                    EmuFileName = "NO$PSX"; break;
                case 4:
                    EmuFileName = "MF2"; break;
                default:
                    break;
            }

            if (Process.GetProcessesByName(EmuFileName).Length > 0)
                PSXProcess = Process.GetProcessesByName(EmuFileName)[0];
            return PSXProcess != null;
        }

        private void KillAttach()
        {
            PSXProcess = null;
            EmuFileName = " ";
            MainTime.Stop();
            MR2AVValueUpdate.Text = "Start Viewer";
            bViewingMR2 = false;
            emuLoaded = false;
            MonLIFGRBox.Text = ""; MonLIFGRBox.BackColor = SystemColors.Control;
            MonPOWGRBox.Text = ""; MonPOWGRBox.BackColor = SystemColors.Control;
            MonINTGRBox.Text = ""; MonINTGRBox.BackColor = SystemColors.Control;
            MonSKLGRBox.Text = ""; MonSKLGRBox.BackColor = SystemColors.Control;
            MonSPDGRBox.Text = ""; MonSPDGRBox.BackColor = SystemColors.Control;
            MonDEFGRBox.Text = ""; MonDEFGRBox.BackColor = SystemColors.Control;
            MonArenaSpeedBox.Text = ""; MonArenaSpeedBox.BackColor = SystemColors.Control;
            MonFameBox.Text = ""; MonFameBox.BackColor = SystemColors.Control;
            MonAgeBox.Text = "";
            MonFormBox.Text = "";
            MonSpoilBox.Text = "";
            MonFearBox.Text = "";
            MonLoyaltyBox.Text = "";
            MonStressBox.Text = "";
            MonFatigueBox.Text = "";
            MonLifeIndexBox.Text = "";
            MonNatureBox.Text = "";
            MonNatureModBox.Text = "";
            MainBreedLabel.Text = "None";
            SubBreedLabel.Text = "None";
            MonGutsRateBox.Text = ""; MonGutsRateBox.BackColor = SystemColors.Control;
            MonSpecialsBox.Text = ""; MonSpecialsBox.BackColor = SystemColors.Control;
            MonLikeItemBox.Text = ""; MonLikeItemBox.BackColor = SystemColors.Control;
            MonDislikeItemBox.Text = ""; MonDislikeItemBox.BackColor = SystemColors.Control;
            MonPlayBox.Text = "";
            MonLifeStageBox.Text = "";
            MonLifeTypeBox.Text = "";
            MonLifespanBox.Text = "";
            MonInitLifespanBox.Text = "";
            MonMotiveBox1.Text = ""; MonMotiveBox1.BackColor = SystemColors.Control;
            MonMotiveBox2.Text = ""; MonMotiveBox2.BackColor = SystemColors.Control;
            MonMotiveBox3.Text = ""; MonMotiveBox3.BackColor = SystemColors.Control;
            MonMotiveBox4.Text = ""; MonMotiveBox4.BackColor = SystemColors.Control;
            MonMotiveBox5.Text = ""; MonMotiveBox5.BackColor = SystemColors.Control;
            MonMotiveBox6.Text = ""; MonMotiveBox6.BackColor = SystemColors.Control;
            MonMotiveBox7.Text = ""; MonMotiveBox7.BackColor = SystemColors.Control;
            MonMotiveBox8.Text = ""; MonMotiveBox8.BackColor = SystemColors.Control;
            MonMotiveBox9.Text = ""; MonMotiveBox9.BackColor = SystemColors.Control;
            MonMotiveBox10.Text = ""; MonMotiveBox10.BackColor = SystemColors.Control;
            MonBreedNameBox.BackColor = SystemColors.Control;
            MonLifeIndexBox.BackColor = SystemColors.Control;
            MonNatureModBox.BackColor = SystemColors.Control;
            MonNatureModBox.ForeColor = SystemColors.ControlText;
            MonBreedNameBox.Text = "No Monster";
            MonGivenNameBox.Text = "";
            MonLifBox.Text = "";
            MonPowBox.Text = "";
            MonIntBox.Text = "";
            MonSkiBox.Text = "";
            BaseStatTotal.Text = "";
            MonDefBox.Text = "";
            MonSpdBox.Text = "";
            MoneyBox.Text = "0G";
            EmuSelectBox.Text = "";
            NextSaleWksBox.Text = "0w";
            MonGoldPeachBox.Checked = false;
            MonSilverPeachBox.Checked = false;
            MonItemUsedBox.Checked = false;
            EmuSelectBox.Enabled = true;
            EmuAttachButton.Enabled = false;
            LIWButton.Enabled = false;
            TWButton.Enabled = false;
            MVButton.Enabled = false;
            MRDebugButton.Enabled = false;
            ItemViewButton.Enabled = false;
            MonCJLabel.Hide();
            MonCJBox.Hide();
            MonCocoonReady.Hide();
            CocoonInfo.Hide();
            EmuSelectBox.SelectedIndex = -1;
            MR2Mode.SelectedIndex = -1;
            Array.Clear(ScratchData, 0, 4);
            Array.Clear(nameToWrite, 0, 24); //bedeg
            EmuVer = -1;
            MR2Mode.SelectedIndex = -1;
            MR2Mode.Enabled = true;
            bJPNMode = false;
            Text = ReadableVersion;

            if (LIW != null)
            {
                LIW.Close();
                LIW = null;
            }
            if (MMW != null)
            {
                MMW.Close();
                MMW = null;
            }
            if (TW != null)
            {
                TW.Close();
                TW = null;
            }
            if (cv != null)
            {
                cv.Close();
                cv = null;
            }
            if (mDBG != null)
            {
                mDBG.Close();
                mDBG = null;
            }
            if (il != null)
            {
                il.Close();
                il = null;
            }
        }

        //private void ViewerWindow_Load(object sender, EventArgs e)
        private async void ViewerWindow_Load(object sender, EventArgs e)
        {
            int FrameID = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full", "Release", null);
            if (FrameID < 461808) // 4.7.2 (Win 10 update)
            {
                MessageBox.Show(@"Please install .net Framework 4.7.2 or higher to use this program. Link is in the Readme. 

(You are currently running " + HumanNETFramework(FrameID) + ")", "MR2AV Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Forms.Application.Exit();
            }

            bShowingExtras = true;
            CycleFeatureDisplay();
            await CheckGitHubNewerVersion();

            EmuSelectBox.SelectedIndex = -1;
            MR2Mode.SelectedIndex = -1;
            Text = ReadableVersion;

            ScumTip.SetToolTip(EXFeaturesChkBox, @"Show/Hide the extra features of MR2AV.
This replaces the old button, skipping the additional window and saving Lexi a lot of code headache.");
            ScumTip.SetToolTip(MonGPSCheck, "Switches Guts Rate display between Frames per Guts point, and Guts per Second.");
        }

        private string HumanNETFramework(int netFrameID)
        {
            switch (netFrameID)
            {
                case 378389:
                    return ".net Framework 4.5.0";
                case 378675:
                    return ".net Framework 4.5.1 (Win 8.1/Server 2012 R2)";
                case 378758:
                    return ".net Framework 4.5.1";
                case 379893:
                    return ".net Framework 4.5.2";
                case 393295:
                    return ".net Framework 4.6 (Win 10)";
                case 393297:
                    return ".net Framework 4.6";
                case 394254:
                    return ".net Framework 4.6.1 (Win 10 Nov. Update)";
                case 394271:
                    return ".net Framework 4.6.1";
                case 394802:
                    return ".net Framework 4.6.2 (Win 10 Anniversary/Server 2016)";
                case 394806:
                    return ".net Framework 4.6.2";
                case 460798:
                    return ".net Framework 4.7.0 (Win 10 Creators Update)";
                case 460805:
                    return ".net Framework 4.7.0";
                case 461308:
                    return ".net Framework 4.7.1 (Win 10 Fall Creators Update)";
                case 461310:
                    return ".net Framework 4.7.1";
                case 461808:
                    return ".net Framework 4.7.2 (Win 10 Apr. 2018 Update)";
                case 461814:
                    return ".net Framework 4.7.2";
                case 528040:
                    return ".net Framework 4.8 (Win 10 May 2019 Update)";
                case 528049:
                    return ".net Framework 4.8";
                default:
                    return "Unrecognised .net Framework ID: ( " + netFrameID + " )";
            }

        }

        private void CollateMonMoves()
        {
            if (EmuVer == DXSelectionID)
            {
                for (int i = 0; i < 24; i++) // Thanks to bedeg for this <3
                {
                    Mon_Moves[i] = MR2ReadInt(0x00097BA2 + (2 * i));
                    Mon_MoveUsed[i] = MR2ReadInt(0x00097BA3 + (2 * i));
                }
            }
            else
            {
                if (MR2Mode.SelectedIndex < 2)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        Mon_Moves[i] = MR2ReadInt(0x00097B9A + (2 * i));
                        Mon_MoveUsed[i] = MR2ReadInt(0x00097B9B + (2 * i));
                    }
                }
                else if (MR2Mode.SelectedIndex == 2)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        Mon_Moves[i] = MR2ReadInt(0x00095B4A + (2 * i));
                        Mon_MoveUsed[i] = MR2ReadInt(0x00095B4B + (2 * i));
                    }
                }
            }
        }

        private void ListItems()
        {
            if (EmuVer == DXSelectionID)
            {
                for (int i = 0; i < 20; i++)
                {
                    //ItemList[i] = MR2ReadInt(0x0009923C + (4 * i));
                    ItemList[i] = MR2ReadInt(0x0009A7EC + (4 * i)); //bedeg
                }
            }
            else
            {
                if (MR2Mode.SelectedIndex < 2)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        ItemList[i] = MR2ReadInt(0x0009923C + (4 * i));
                    }
                }
                else if (MR2Mode.SelectedIndex == 2)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        ItemList[i] = MR2ReadInt(0x000971EC + (4 * i)); // 971EC
                    }
                }
            }
        }

        private void EmuSelectBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //765A7346
            int PSBase;
            EmuVer = EmuSelectBox.SelectedIndex;

            if (EmuSelectBox.SelectedIndex >= 0)
            {
                emuLoaded = GrabPSXProcess();
                if (emuLoaded)
                {
                    psxPTR = OpenProcess(PROCESS_ALLACCESS, false, PSXProcess.Id);
                    PSBase = (int)PSXProcess.MainModule.BaseAddress;
                    EmuAttachButton.Text = "Detach";
                    EmuSelectBox.Enabled = false;
                    UnfreezeTicks = 4;

                    MainTime.Interval = UpdateRate.Value * 125; ; // specify interval time as you want
                    MainTime.Start();
                    int PointOffset;
                    switch (EmuVer)
                    {
                        case 0: // ePSXe: check pointer at 0x0004E8E8
                            ReadProcessMemory(psxPTR, PSBase + 0x0004e8e8, ScratchData, 4, ref HasRead);
                            PointOffset = BitConverter.ToInt32(ScratchData, 0);
                            PSXBase = PointOffset;
                            break;
                        case 1: // pSX: Check pointer at 0x00571A5C.
                            ReadProcessMemory(psxPTR, 0x00571A5C, ScratchData, 4, ref HasRead);
                            PointOffset = BitConverter.ToInt32(ScratchData, 0);
                            PSXBase = PointOffset;
                            break;
                        case 2: // XEBRA latest: Check pointer at 0x000A5DF8.
                            ReadProcessMemory(psxPTR, 0x004A5DF8, ScratchData, 4, ref HasRead);
                            PointOffset = BitConverter.ToInt32(ScratchData, 0);
                            PSXBase = PointOffset;
                            break;
                        case 3: // NOCashPSX: "NO$PSX.EXE" + 00091C80
                            ReadProcessMemory(psxPTR, PSBase + 0x00091C80, ScratchData, 4, ref HasRead);
                            PointOffset = BitConverter.ToInt32(ScratchData, 0);
                            PSXBase = PointOffset;
                            break;
                        case 4: // MR2DX: "MF2.exe" + 002DEC6C (EN) OR 0x002CA504 (JPN)
                            DialogResult dialogResult = MessageBox.Show(@"あの… [MF2DX] と [MR2DX] のどちらを使っていますか?
（［MF2DX］の場合は［はい/Yes］を押してください）

Erm, are you playing MF2DX, or MR2DX?
(If you're playing MF2DX, press Yes. Otherwise press No.)
", ReadableVersion + "/" + ReadableVersionJP, MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                PSXBase = PSBase + 0x002CA504; // 1.001 update
                                bJPNMode = true;
                                // arigatou, nyori-san. @NyoriMF2 on Twitter
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                PSXBase = PSBase + 0x002DEC6C; // 1.001 update
                                bJPNMode = false;
                            }
                            MR2Mode.SelectedIndex = -1;
                            MR2Mode.Enabled = false;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("MR2 Advanced Viewer cannot find " + EmuSelectBox.Text + " running on this system. Please run the selected emulator, or try another.", "MR2AV Attach Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    EmuSelectBox.SelectedIndex = -1;
                    EmuVer = -1;
                }
            }
        }

        private void EmuAttachButton_Click(object sender, EventArgs e)
        {
            KillAttach();
        }

        private void MR2AVValueUpdate_Click(object sender, EventArgs e)
        {
            if (!emuLoaded)
                return;

            bViewingMR2 = !bViewingMR2;

            if (bViewingMR2)
            {
                MR2AVValueUpdate.Text = "Stop Viewer";
            }
            else
            {
                MR2AVValueUpdate.Text = "Start Viewer";
            }
            LIWButton.Enabled = bViewingMR2;
            TWButton.Enabled = bViewingMR2;
            //          IVButton.Enabled = bViewingMR2;
            MVButton.Enabled = bViewingMR2;
            MRDebugButton.Enabled = bViewingMR2;
            ItemViewButton.Enabled = bViewingMR2;
        }

        private void MR2LIWeeks()
        {
            string IndexWeeks;
            int LifeIndexINT = (Mon_Fatigue + (Mon_Stress * 2));

            IndexWeeks = LifeIndexINT.ToString();
            if (LifeIndexINT > 279)
            {
                IndexWeeks += " (-7 Weeks)";
                MonLifeIndexBox.BackColor = Color.Red;
            }
            else if (LifeIndexINT > 244)
            {
                IndexWeeks += " (-6 Weeks)";
                MonLifeIndexBox.BackColor = Color.OrangeRed;
            }
            else if (LifeIndexINT > 209)
            {
                IndexWeeks += " (-5 Weeks)";
                MonLifeIndexBox.BackColor = Color.OrangeRed;
            }
            else if (LifeIndexINT > 174)
            {
                IndexWeeks += " (-4 Weeks)";
                MonLifeIndexBox.BackColor = Color.Orange;
            }
            else if (LifeIndexINT > 139)
            {
                IndexWeeks += " (-3 Weeks)";
                MonLifeIndexBox.BackColor = Color.Orange;
            }
            else if (LifeIndexINT > 104)
            {
                IndexWeeks += " (-2 Weeks)";
                MonLifeIndexBox.BackColor = Color.LightYellow;
            }
            else if (LifeIndexINT > 69)
            {
                IndexWeeks += " (-1 Week)";
                MonLifeIndexBox.BackColor = Color.LightYellow;
            }
            else
            {
                IndexWeeks += " (-0 Weeks)";
                MonLifeIndexBox.BackColor = Color.LightGreen;
            }
            MonLifeIndexBox.Invoke((MethodInvoker)delegate { MonLifeIndexBox.Text = IndexWeeks; });
        }

        public string[] MR2ReadDataFile(string relPath) // Example usage: MR2ReadDataFile(@"data\en_van\filename.txt");
        {
            string[] loadedArray = null;
            string finalPath;

            finalPath = System.IO.Path.GetFullPath(relPath);
            if (System.IO.File.Exists(finalPath))
                loadedArray = System.IO.File.ReadAllLines(finalPath);
            else
            {
                MR2AVValueUpdate_Click(null, null);
                MessageBox.Show("Error: Could not load file: " + finalPath + @" . Please check your directories.

As a precaution, MR2AV has stopped reading from the emulator. To continue, press Start Viewer again.", "MR2AV File Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return loadedArray;
        }

        private void HandleMotiveBox(TextBox boxID, int motivPercent)
        {
            boxID.Invoke((MethodInvoker)delegate { boxID.Text = motivPercent + "%"; });
            if (motivPercent < 30 && motivPercent > -1)
                boxID.BackColor = Color.OrangeRed;
            else if (motivPercent < 81)
                boxID.BackColor = Color.LightYellow;
            else if (motivPercent >= 81 && motivPercent < 101)
                boxID.BackColor = Color.LightGreen;
            else
                boxID.BackColor = SystemColors.Control;
        }

        private string MRLifeTypeOutput()
        {
            switch (Mon_LifeType)
            {
                case 0: return "Type 1: Normal";
                case 1: return "Type 2: Precocious";
                case 2: return "Type 3: Late Bloom";
                case 3: return "Type 4: Sustainable";
                case 4: return "Type 5: Prodigy [X]";
                default: return "Unrecognised";
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (UnfreezeTicks > 0)
            {
                UnfreezeTicks--;
                if (UnfreezeTicks <= 1 && EmuAttachButton.Enabled == false)
                    EmuAttachButton.Enabled = true;
            }
            if (emuLoaded && bViewingMR2)
            {
                if (EmuVer != -1)
                {
                    if (Process.GetProcessesByName(EmuFileName).Length <= 0)
                        KillAttach();

                    if (EmuVer == DXSelectionID) // MR2DX changed locations
                    {
                        // All -8 as of 1.0.0.1
                        Mon_Age = MR2ReadDouble(0x00097A0C);
                        MonGenus_Main = MR2ReadInt(0x00097A10);
                        MonGenus_Sub = MR2ReadInt(0x00097A14);
                        Mon_Lif = MR2ReadDouble(0x00097A18);
                        Mon_Pow = MR2ReadDouble(0x00097A1A);
                        Mon_Def = MR2ReadDouble(0x00097A1C);
                        Mon_Skl = MR2ReadDouble(0x00097A1E);
                        Mon_Spd = MR2ReadDouble(0x00097A20);
                        Mon_Int = MR2ReadDouble(0x00097A22);
                        Mon_Lifespan = MR2ReadDouble(0x00097A28);
                        Mon_InitLifespan = MR2ReadDouble(0x00097A2A);
                        Mon_Nature = MR2ReadDouble(0x00097A2C);
                        Mon_Fatigue = MR2ReadInt(0x00097A2F);
                        Mon_Fame = MR2ReadInt(0x00097A30);
                        Mon_Stress = MR2ReadInt(0x00097A33); // Stress is signed.
                        Mon_LoyalSpoil = MR2ReadInt(0x00097A34);
                        Mon_LoyalFear = MR2ReadInt(0x00097A35);
                        Mon_Form = MR2ReadInt(0x00097A36);
                        MonGR_Power = MR2ReadInt(0x00097A38);
                        MonGR_Intelligence = MR2ReadInt(0x00097A39);
                        MonGR_LIF = MR2ReadInt(0x00097A3A);
                        MonGR_Skill = MR2ReadInt(0x00097A3B);
                        MonGR_Speed = MR2ReadInt(0x00097A3C);
                        MonGR_Defence = MR2ReadInt(0x00097A3D);
                        Mon_NatureBase = MR2ReadInt(0x00097A51);
                        Mon_TrainBoost = MR2ReadDouble(0x00097A70);
                        Mon_CupJellies = MR2ReadInt(0x00097ABD);
                        MonPeach_Gold = MR2ReadBool(0x00097ABE);
                        MonPeach_Silver = MR2ReadBool(0x00097ABF);
                        Mon_MotivDom = MR2ReadInt(0x00097AE8);
                        Mon_MotivStu = MR2ReadInt(0x00097AE9);
                        Mon_MotivRun = MR2ReadInt(0x00097AEA);
                        Mon_MotivSho = MR2ReadInt(0x00097AEB);
                        Mon_MotivDod = MR2ReadInt(0x00097AEC);
                        Mon_MotivEnd = MR2ReadInt(0x00097AED);
                        Mon_MotivPul = MR2ReadInt(0x00097AEE);
                        Mon_MotivMed = MR2ReadInt(0x00097AEF);
                        Mon_MotivLea = MR2ReadInt(0x00097AF0);
                        Mon_MotivSwi = MR2ReadInt(0x00097AF1);
                        Mon_PlayType = MR2ReadInt(0x00097AF5);
                        Mon_Drug = MR2ReadInt(0x00097B02);
                        Mon_DrugDuration = MR2ReadInt(0x00097B04);
                        Mon_ItemUsed = MR2ReadBool(0x00097B06);
                        MonItem_Like = MR2ReadInt(0x00097B12);
                        MonItem_Dislike = MR2ReadInt(0x00097B14);

                        // All +8 as of 1.0.0.1
                        Mon_Rank = MR2ReadInt(0x00097B9A);
                        Mon_LifeStage = MR2ReadInt(0x00097B9B);
                        Mon_LifeType = MR2ReadInt(0x00097B9C);
                        Mon_ArenaSPD = MR2ReadInt(0x00097BDE);
                        Mon_GutsRate = MR2ReadInt(0x00097BDF); // -7 hex from this, for active move selection.
                        ActiveMoves[0] = MR2ReadInt(0x00097BD8);
                        ActiveMoves[1] = MR2ReadInt(0x00097BD9);
                        ActiveMoves[2] = MR2ReadInt(0x00097BDA);
                        ActiveMoves[3] = MR2ReadInt(0x00097BDB);

                        // I have no idea where these are stored.
                        Game_NextSale = MR2ReadInt(0x0009A88E);
                        Game_ErrantryCD = MR2ReadInt(0x0009A899);
                    }
                    else
                    {
                        if (MR2Mode.SelectedIndex < 2)
                        {
                            Mon_Age = MR2ReadDouble(0x00097A14);
                            MonGenus_Main = MR2ReadInt(0x00097A18);
                            MonGenus_Sub = MR2ReadInt(0x00097A1C);
                            Mon_Lif = MR2ReadDouble(0x00097A20);
                            Mon_Pow = MR2ReadDouble(0x00097A22);
                            Mon_Int = MR2ReadDouble(0x00097A2A);
                            Mon_Skl = MR2ReadDouble(0x00097A26);
                            Mon_Spd = MR2ReadDouble(0x00097A28);
                            Mon_Def = MR2ReadDouble(0x00097A24);
                            Mon_Lifespan = MR2ReadDouble(0x00097A30);
                            Mon_InitLifespan = MR2ReadDouble(0x00097A32);
                            Mon_Nature = MR2ReadDouble(0x00097A34);
                            Mon_Fatigue = MR2ReadInt(0x00097A37);
                            Mon_Fame = MR2ReadInt(0x00097A38);
                            Mon_Stress = MR2ReadInt(0x00097A3B); // Stress is signed.
                            Mon_LoyalSpoil = MR2ReadInt(0x00097A3C);
                            Mon_LoyalFear = MR2ReadInt(0x00097A3D);
                            Mon_Form = MR2ReadInt(0x00097A3E);
                            MonGR_Power = MR2ReadInt(0x00097A40);
                            MonGR_Intelligence = MR2ReadInt(0x00097A41);
                            MonGR_LIF = MR2ReadInt(0x00097A42);
                            MonGR_Skill = MR2ReadInt(0x00097A43);
                            MonGR_Speed = MR2ReadInt(0x00097A44);
                            MonGR_Defence = MR2ReadInt(0x00097A45);
                            Mon_NatureBase = MR2ReadInt(0x00097A59);
                            Mon_TrainBoost = MR2ReadDouble(0x00097A78);
                            Mon_CupJellies = MR2ReadInt(0x00097AC5);
                            MonPeach_Gold = MR2ReadBool(0x00097AC6);
                            MonPeach_Silver = MR2ReadBool(0x00097AC7);
                            Mon_MotivDom = MR2ReadInt(0x00097AF0);
                            Mon_MotivStu = MR2ReadInt(0x00097AF1);
                            Mon_MotivRun = MR2ReadInt(0x00097AF2);
                            Mon_MotivSho = MR2ReadInt(0x00097AF3);
                            Mon_MotivDod = MR2ReadInt(0x00097AF4);
                            Mon_MotivEnd = MR2ReadInt(0x00097AF5);
                            Mon_MotivPul = MR2ReadInt(0x00097AF6);
                            Mon_MotivMed = MR2ReadInt(0x00097AF7);
                            Mon_MotivLea = MR2ReadInt(0x00097AF8);
                            Mon_MotivSwi = MR2ReadInt(0x00097AF9);
                            Mon_PlayType = MR2ReadInt(0x00097AFD);
                            Mon_Drug = MR2ReadInt(0x00097B0A);
                            Mon_DrugDuration = MR2ReadInt(0x00097B0C);
                            Mon_ItemUsed = MR2ReadBool(0x00097B0E);
                            MonItem_Like = MR2ReadInt(0x00097B1A);
                            MonItem_Dislike = MR2ReadInt(0x00097B1C);
                            Mon_Rank = MR2ReadInt(0x00097B92);
                            Mon_LifeStage = MR2ReadInt(0x00097B93);
                            Mon_LifeType = MR2ReadInt(0x00097B94);
                            ActiveMoves[0] = MR2ReadInt(0x00097BD0);
                            ActiveMoves[1] = MR2ReadInt(0x00097BD1);
                            ActiveMoves[2] = MR2ReadInt(0x00097BD2);
                            ActiveMoves[3] = MR2ReadInt(0x00097BD3);
                            Mon_ArenaSPD = MR2ReadInt(0x00097BD6);
                            Mon_GutsRate = MR2ReadInt(0x00097BD7); // -7 hex from this, for active move selection.
                            Game_NextSale = MR2ReadInt(0x000992DE);
                            Game_ErrantryCD = MR2ReadInt(0x000992E9);
                        }
                        else if (MR2Mode.SelectedIndex == 2)
                        {
                            Mon_Age = MR2ReadDouble(0x000959C4);
                            MonGenus_Main = MR2ReadInt(0x000959C8); // MuffinTrain woz ere (15/01/2023)
                            MonGenus_Sub = MR2ReadInt(0x000959CC);
                            Mon_Lif = MR2ReadDouble(0x000959D0);
                            Mon_Pow = MR2ReadDouble(0x000959D2);
                            Mon_Int = MR2ReadDouble(0x000959DA);
                            Mon_Skl = MR2ReadDouble(0x000959D6);
                            Mon_Spd = MR2ReadDouble(0x000959D8);
                            Mon_Def = MR2ReadDouble(0x000959D4);
                            Mon_Lifespan = MR2ReadDouble(0x000959E0);
                            Mon_InitLifespan = MR2ReadDouble(0x000959E2);
                            Mon_Nature = MR2ReadDouble(0x000959E4);
                            Mon_Fatigue = MR2ReadInt(0x000959E7);
                            Mon_Fame = MR2ReadInt(0x000959E8);
                            Mon_Stress = MR2ReadInt(0x000959EB); // Stress is signed.
                            Mon_LoyalSpoil = MR2ReadInt(0x000959EC);
                            Mon_LoyalFear = MR2ReadInt(0x000959ED);
                            Mon_Form = MR2ReadInt(0x000959EE);
                            MonGR_Power = MR2ReadInt(0x000959F0);
                            MonGR_Intelligence = MR2ReadInt(0x000959F1);
                            MonGR_LIF = MR2ReadInt(0x000959F2);
                            MonGR_Skill = MR2ReadInt(0x000959F3);
                            MonGR_Speed = MR2ReadInt(0x000959F4);
                            MonGR_Defence = MR2ReadInt(0x000959F5);
                            Mon_NatureBase = MR2ReadInt(0x00095A09);
                            Mon_TrainBoost = MR2ReadDouble(0x00095A28); 
                            Mon_CupJellies = MR2ReadInt(0x00095A75);
                            MonPeach_Gold = MR2ReadBool(0x00095A76);
                            MonPeach_Silver = MR2ReadBool(0x00095A77);
                            Mon_MotivDom = MR2ReadInt(0x00095AA0);
                            Mon_MotivStu = MR2ReadInt(0x00095AA1);
                            Mon_MotivRun = MR2ReadInt(0x00095AA2);
                            Mon_MotivSho = MR2ReadInt(0x00095AA3);
                            Mon_MotivDod = MR2ReadInt(0x00095AA4);
                            Mon_MotivEnd = MR2ReadInt(0x00095AA5);
                            Mon_MotivPul = MR2ReadInt(0x00095AA6);
                            Mon_MotivMed = MR2ReadInt(0x00095AA7);
                            Mon_MotivLea = MR2ReadInt(0x00095AA8);
                            Mon_MotivSwi = MR2ReadInt(0x00095AA9);
                            Mon_PlayType = MR2ReadInt(0x00095AAD);
                            Mon_Drug = MR2ReadInt(0x00095ABA);
                            Mon_DrugDuration = MR2ReadInt(0x00095ABC);
                            Mon_ItemUsed = MR2ReadBool(0x00095ABE);
                            MonItem_Like = MR2ReadInt(0x00095ACA);
                            MonItem_Dislike = MR2ReadInt(0x00095ACC); ///
                            Mon_Rank = MR2ReadInt(0x00095B42);
                            Mon_LifeStage = MR2ReadInt(0x00095B43);
                            Mon_LifeType = MR2ReadInt(0x00095B44);
                            ActiveMoves[0] = MR2ReadInt(0x00095B80);
                            ActiveMoves[1] = MR2ReadInt(0x00095B81);
                            ActiveMoves[2] = MR2ReadInt(0x00095B82);
                            ActiveMoves[3] = MR2ReadInt(0x00095B83);
                            Mon_ArenaSPD = MR2ReadInt(0x00095B86);
                            Mon_GutsRate = MR2ReadInt(0x00095B87);
                            Game_NextSale = MR2ReadInt(0x0009728E);
                            Game_ErrantryCD = MR2ReadInt(0x00097299);
                        }
                    }

                    if (Mon_NatureBase >= 128) { Mon_NatureBase = (256 - Mon_NatureBase) * -1; } // Nature is signed.

                    if (Mon_Nature >= 32768) { Mon_Nature = (65536 - Mon_Nature) * -1; } // Nature... modification? is signed.

                    if (Mon_Form >= 128) { Mon_Form = (256 - Mon_Form) * -1; } // Form is signed.

                    if (Mon_Fatigue > 100 && Mon_Fatigue < 128)// Fatigue is signed.
                        Mon_Fatigue = 100;
                    else if (Mon_Fatigue > 128)
                        Mon_Fatigue = 0;

                    IsErrantrySaleReady();

                    float fMGR = Mon_GutsRate;
                    if (fMGR > 0)
                        fMGR = 30 / fMGR;

                    Mon_EffDef = Mon_Def * (1 + ((float)Mon_Form / 400));
                    Mon_EffDef = Math.Min(Mon_EffDef, 999);
                    Mon_EffDef = Math.Max(Mon_EffDef, 1);
                    Mon_EffSpd = Mon_Spd * (1 - ((float)Mon_Form / 400));
                    Mon_EffSpd = Math.Min(Mon_EffSpd, 999);
                    Mon_EffSpd = Math.Max(Mon_EffSpd, 1);

                    // Growth Rates
                    if (!HideData.Checked)
                    {
                        if (!MonGRNumToggle.Checked)
                        {
                            MonLIFGRBox.Invoke((MethodInvoker)delegate { MonLIFGRBox.Text = MonGRAlphabetise(MonGR_LIF, MonLIFGRBox) + " / " + (MonGR_LIF + 1); });
                            MonPOWGRBox.Invoke((MethodInvoker)delegate { MonPOWGRBox.Text = MonGRAlphabetise(MonGR_Power, MonPOWGRBox) + " / " + (MonGR_Power + 1); });
                            MonINTGRBox.Invoke((MethodInvoker)delegate { MonINTGRBox.Text = MonGRAlphabetise(MonGR_Intelligence, MonINTGRBox) + " / " + (MonGR_Intelligence + 1); });
                            MonSKLGRBox.Invoke((MethodInvoker)delegate { MonSKLGRBox.Text = MonGRAlphabetise(MonGR_Skill, MonSKLGRBox) + " / " + (MonGR_Skill + 1); });
                            MonSPDGRBox.Invoke((MethodInvoker)delegate { MonSPDGRBox.Text = MonGRAlphabetise(MonGR_Speed, MonSPDGRBox) + " / " + (MonGR_Speed + 1); });
                            MonDEFGRBox.Invoke((MethodInvoker)delegate { MonDEFGRBox.Text = MonGRAlphabetise(MonGR_Defence, MonDEFGRBox) + " / " + (MonGR_Defence + 1); });
                        }
                        else
                        {
                            MonLIFGRBox.Invoke((MethodInvoker)delegate { MonLIFGRBox.Text = MonGRAlphabetise(MonGR_LIF, MonLIFGRBox) + " / " + MonGR_LIF; });
                            MonPOWGRBox.Invoke((MethodInvoker)delegate { MonPOWGRBox.Text = MonGRAlphabetise(MonGR_Power, MonPOWGRBox) + " / " + MonGR_Power; });
                            MonINTGRBox.Invoke((MethodInvoker)delegate { MonINTGRBox.Text = MonGRAlphabetise(MonGR_Intelligence, MonINTGRBox) + " / " + MonGR_Intelligence; });
                            MonSKLGRBox.Invoke((MethodInvoker)delegate { MonSKLGRBox.Text = MonGRAlphabetise(MonGR_Skill, MonSKLGRBox) + " / " + MonGR_Skill; });
                            MonSPDGRBox.Invoke((MethodInvoker)delegate { MonSPDGRBox.Text = MonGRAlphabetise(MonGR_Speed, MonSPDGRBox) + " / " + MonGR_Speed; });
                            MonDEFGRBox.Invoke((MethodInvoker)delegate { MonDEFGRBox.Text = MonGRAlphabetise(MonGR_Defence, MonDEFGRBox) + " / " + MonGR_Defence; });
                        }
                    }
                    else
                    {
                        MonLIFGRBox.Text = "---"; MonLIFGRBox.BackColor = SystemColors.Control;
                        MonPOWGRBox.Text = "---"; MonPOWGRBox.BackColor = SystemColors.Control;
                        MonINTGRBox.Text = "---"; MonINTGRBox.BackColor = SystemColors.Control;
                        MonSKLGRBox.Text = "---"; MonSKLGRBox.BackColor = SystemColors.Control;
                        MonSPDGRBox.Text = "---"; MonSPDGRBox.BackColor = SystemColors.Control;
                        MonDEFGRBox.Text = "---"; MonDEFGRBox.BackColor = SystemColors.Control;
                    }
                    // General Stats
                    MonAgeBox.Invoke((MethodInvoker)delegate { MonAgeBox.Text = MonAgeDisplay(); });
                    MonFormBox.Invoke((MethodInvoker)delegate { MonFormBox.Text = Mon_Form + " (" + MonFormDescript() + ")"; });
                    MonSpoilBox.Invoke((MethodInvoker)delegate { MonSpoilBox.Text = Mon_LoyalSpoil.ToString(); });
                    MonFearBox.Invoke((MethodInvoker)delegate { MonFearBox.Text = Mon_LoyalFear.ToString(); });
                    MonLifBox.Invoke((MethodInvoker)delegate { MonLifBox.Text = Mon_Lif.ToString(); });
                    MonPowBox.Invoke((MethodInvoker)delegate { MonPowBox.Text = Mon_Pow.ToString(); });
                    MonIntBox.Invoke((MethodInvoker)delegate { MonIntBox.Text = Mon_Int.ToString(); });
                    MonSkiBox.Invoke((MethodInvoker)delegate { MonSkiBox.Text = Mon_Skl.ToString(); });
                    BaseStatTotal.Invoke((MethodInvoker)delegate { BaseStatTotal.Text = (Mon_Lif + Mon_Pow + Mon_Int + Mon_Skl + Mon_Def + Mon_Spd).ToString(); });
                    MonDefBox.Invoke((MethodInvoker)delegate { MonDefBox.Text = Mon_Def + "(" + (int)Mon_EffDef + ")"; });
                    MonSpdBox.Invoke((MethodInvoker)delegate { MonSpdBox.Text = Mon_Spd + "(" + (int)Mon_EffSpd + ")"; });

                    MonLoyaltyBox.Invoke((MethodInvoker)delegate { MonLoyaltyBox.Text = (Mon_LoyalSpoil / 2 + Mon_LoyalFear / 2).ToString(); });

                    MonFameBox.Invoke((MethodInvoker)delegate { MonFameBox.Text = Mon_Fame.ToString(); });
                    if (Mon_Fame > 100)
                        MonFameBox.BackColor = Color.HotPink;
                    else
                        MonFameBox.BackColor = SystemColors.Control;
                    MonStressBox.Invoke((MethodInvoker)delegate { MonStressBox.Text = Mon_Stress.ToString(); });
                    MonFatigueBox.Invoke((MethodInvoker)delegate { MonFatigueBox.Text = Mon_Fatigue.ToString(); });
                    MR2LIWeeks();
                    MonNatureBox.Invoke((MethodInvoker)delegate { MonNatureBox.Text = Mon_NatureBase.ToString(); });
                    MonNatureModBox.Invoke((MethodInvoker)delegate { MonNatureModBox.Text = MonNature_Display(); });
                    MainBreedLabel.Invoke((MethodInvoker)delegate { MainBreedLabel.Text = MonGenusNames(MonGenus_Main, true); });
                    SubBreedLabel.Invoke((MethodInvoker)delegate { SubBreedLabel.Text = MonGenusNames(MonGenus_Sub, false); });
                    if (Mon_GutsRate >= 50)
                    {
                        if (MonGPSCheck.Checked)
                            MonGutsRateBox.Invoke((MethodInvoker)delegate { MonGutsRateBox.Text = "30.00"; });
                        else
                            MonGutsRateBox.Invoke((MethodInvoker)delegate { MonGutsRateBox.Text = Mon_GutsRate.ToString() + " (1)"; });
                    }
                    else
                    {
                        if (MonGPSCheck.Checked)
                            MonGutsRateBox.Invoke((MethodInvoker)delegate { MonGutsRateBox.Text = fMGR.ToString("0.00"); });
                        else
                            MonGutsRateBox.Invoke((MethodInvoker)delegate { MonGutsRateBox.Text = Mon_GutsRate.ToString(); });
                    }
                    MonSpecialsBox.Invoke((MethodInvoker)delegate { MonSpecialsBox.Text = MonReadBattleSpecials(); });
                    MonLikeItemBox.Invoke((MethodInvoker)delegate { MonLikeItemBox.Text = MonDesireNames(MonItem_Like); });
                    if (MonDesireNames(MonItem_Like) == "Tablet" || MonDesireNames(MonItem_Like) == "Battle")
                        MonLikeItemBox.BackColor = Color.LightGreen;
                    else
                        MonLikeItemBox.BackColor = SystemColors.Control;
                    MonDislikeItemBox.Invoke((MethodInvoker)delegate { MonDislikeItemBox.Text = MonDesireNames(MonItem_Dislike); });
                    if (MonDesireNames(MonItem_Dislike) == "Tablet" || MonDesireNames(MonItem_Dislike) == "Battle")
                        MonDislikeItemBox.BackColor = Color.OrangeRed;
                    else
                        MonDislikeItemBox.BackColor = SystemColors.Control;
                    MonPlayBox.Invoke((MethodInvoker)delegate { MonPlayBox.Text = MonPlaytimeNames(Mon_PlayType); });
                    if (Mon_Lifespan == 0 /* || [some check for insta-death via battle] */)
                        MonLifeStageBox.Invoke((MethodInvoker)delegate { MonLifeStageBox.Text = MonLifeStageNames(11); });
                    else
                        MonLifeStageBox.Invoke((MethodInvoker)delegate { MonLifeStageBox.Text = MonLifeStageNames(Mon_LifeStage); });
                    MonLifeTypeBox.Invoke((MethodInvoker)delegate { MonLifeTypeBox.Text = MRLifeTypeOutput(); });
                    MonLifespanBox.Invoke((MethodInvoker)delegate { MonLifespanBox.Text = Mon_Lifespan + "w"; });
                    MonInitLifespanBox.Invoke((MethodInvoker)delegate { MonInitLifespanBox.Text = Mon_InitLifespan + "w"; });
                    MonBreedNameBox.Invoke((MethodInvoker)delegate { MonBreedNameBox.Text = MonBreedNames(); });
                    if (!bChangingName)  //bedeg
                        MonGivenNameBox.Invoke((MethodInvoker)delegate { MonGivenNameBox.Text = MonReadGivenName(); });
                    MoneyBox.Text = MonReadMoney();
                    MonPrizeMoney.Text = MonReadPrizeMoney();
                    if (MonBreedNameBox.Text.Contains("[E]") || MonBreedNameBox.Text.Contains("(N/S)"))
                        MonBreedNameBox.BackColor = Color.LightPink;
                    else
                        MonBreedNameBox.BackColor = SystemColors.Control;
                    // Arena Speed
                    MonArenaSpeedBox.Invoke((MethodInvoker)delegate { MonArenaSpeedBox.Text = MonMSAlphabetise(MonArenaSpeedBox); });
                    // Peaches
                    MonGoldPeachBox.Invoke((MethodInvoker)delegate { MonGoldPeachBox.Checked = MonPeach_Gold; });
                    MonSilverPeachBox.Invoke((MethodInvoker)delegate { MonSilverPeachBox.Checked = MonPeach_Silver; });
                    // Item Used?
                    MonItemUsedBox.Invoke((MethodInvoker)delegate { MonItemUsedBox.Checked = Mon_ItemUsed; });
                    // Motivations:
                    if (!HideData.Checked)
                    {
                        HandleMotiveBox(MonMotiveBox1, Mon_MotivDom);
                        HandleMotiveBox(MonMotiveBox2, Mon_MotivStu);
                        HandleMotiveBox(MonMotiveBox3, Mon_MotivRun);
                        HandleMotiveBox(MonMotiveBox4, Mon_MotivSho);
                        HandleMotiveBox(MonMotiveBox5, Mon_MotivDod);
                        HandleMotiveBox(MonMotiveBox6, Mon_MotivEnd);
                        HandleMotiveBox(MonMotiveBox7, Mon_MotivPul);
                        HandleMotiveBox(MonMotiveBox8, Mon_MotivMed);
                        HandleMotiveBox(MonMotiveBox9, Mon_MotivLea);
                        HandleMotiveBox(MonMotiveBox10, Mon_MotivSwi);
                    }
                    else
                    {
                        MonMotiveBox1.Text = "---"; MonMotiveBox1.BackColor = SystemColors.Control;
                        MonMotiveBox2.Text = "---"; MonMotiveBox2.BackColor = SystemColors.Control;
                        MonMotiveBox3.Text = "---"; MonMotiveBox3.BackColor = SystemColors.Control;
                        MonMotiveBox4.Text = "---"; MonMotiveBox4.BackColor = SystemColors.Control;
                        MonMotiveBox5.Text = "---"; MonMotiveBox5.BackColor = SystemColors.Control;
                        MonMotiveBox6.Text = "---"; MonMotiveBox6.BackColor = SystemColors.Control;
                        MonMotiveBox7.Text = "---"; MonMotiveBox7.BackColor = SystemColors.Control;
                        MonMotiveBox8.Text = "---"; MonMotiveBox8.BackColor = SystemColors.Control;
                        MonMotiveBox9.Text = "---"; MonMotiveBox9.BackColor = SystemColors.Control;
                        MonMotiveBox10.Text = "---"; MonMotiveBox10.BackColor = SystemColors.Control;
                    }
                    NextSaleWksBox.Invoke((MethodInvoker)delegate { NextSaleWksBox.Text = Game_NextSale + "w"; });
                    //NextSaleWksBox.Invoke((MethodInvoker)delegate { NextSaleWksBox.Text = Mon_TrainBoost.ToString(); });
                    if (Mon_OldDrug != Mon_Drug)
                    {
                        if (Mon_Drug > 33 && Mon_Drug < 42)
                        {
                            MonDrugsLabel.Text = MonDesireNames(Mon_Drug);
                            MonDrugsWeeks.Text = "(" + Mon_DrugDuration + "w)";
                            MonDrugsWeeks.Left = MonDrugsLabel.Left + MonDrugsLabel.Width;
                            switch (Mon_Drug)
                            {
                                case 34:
                                    ScumTip.SetToolTip(MonDrugsLabel, @"Teromeann:
Combat stimulant. Increases SPD by 100%, and POW by 50% in battle for one week.
-20 Weeks of lifespan on use."); break;
                                case 37:
                                    ScumTip.SetToolTip(MonDrugsLabel, @"Kasseitan:
Combat stimulant. Increases SPD by 50%, and POW by 50% in battle for one week.
-20 Weeks of lifespan on use."); break;
                                case 38:
                                    ScumTip.SetToolTip(MonDrugsLabel, @"Troron:
Training stimulant. Increases POW by 10 and SKI by 5 for each Success/Cheat/Great for four weeks.
-6 Weeks of lifespan on use."); break;
                                case 39:
                                    ScumTip.SetToolTip(MonDrugsLabel, @"Nageel:
Combat stimulant. Increases SKI by 100%, and DEF by 50% in battle for one week.
-20 Weeks of lifespan on use."); break;
                                case 40:
                                    ScumTip.SetToolTip(MonDrugsLabel, @"Torokachin FX:
Training stimulant. Increases POW and SKI by 25 for each Success/Cheat/Great for four weeks.
-12 Weeks of lifespan on use. (Also; the user has edited this drug in.)"); break;
                                case 41:
                                    ScumTip.SetToolTip(MonDrugsLabel, @"Paradoxine:
Training stimulant. Increases POW and SKI by 30 for each Success/Cheat/Great for four weeks.
Each increase also decreases SPD and DEF by 10%.
-18 Weeks of lifespan on use."); break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            MonDrugsLabel.Text = "---";
                            MonDrugsWeeks.Text = "";
                            ScumTip.SetToolTip(MonDrugsLabel, null);
                        }
                    }
                    Mon_OldDrug = Mon_Drug;

                    if (bJPNMode && Text != ReadableVersionJP)
                        Text = ReadableVersionJP;

                    if (!bJPNMode && Text != ReadableVersion)
                        Text = ReadableVersion;

                    if (MonGenus_Main == 36 && Mon_Rank < 3)
                    {
                        MonCJBox.Show();
                        MonCJLabel.Show();
                        MonCocoonReady.Show();
                        CocoonInfo.Show();
                        MonCJBox.Invoke((MethodInvoker)delegate { MonCJBox.Text = Mon_CupJellies.ToString(); });
                        if (Mon_CupJellies >= 30)
                            MonCJBox.BackColor = Color.LightGreen;
                        else
                            MonCJBox.BackColor = SystemColors.Control;

                        if ((Mon_Age >= 192) && ((Mon_LoyalSpoil / 2 + Mon_LoyalFear / 2) >= 80) && Mon_Fatigue == 0 && Mon_Stress < 30)
                            MonCocoonReady.Checked = true;
                        else
                            MonCocoonReady.Checked = false;
                    }
                    else
                    {
                        MonCJBox.Hide();
                        MonCJLabel.Hide();
                        MonCocoonReady.Hide();
                        CocoonInfo.Hide();
                    }
                    if (LIW != null)
                    {
                        LIW.Fatigue = Mon_Fatigue;
                        LIW.Stress = Mon_Stress;
                        LIW.LIBox = MonLifeIndexBox.Text;
                        LIW.boxCol = MonLifeIndexBox.BackColor;
                        LIW.LSR = Mon_Lifespan;
                        LIW.LSM = Mon_InitLifespan;
                    }
                    if (TW != null)
                    {
                        TW.TrainInt = Mon_TrainBoost;
                        TW.StatGRs[0] = MonGR_LIF;
                        TW.StatGRs[1] = MonGR_Power;
                        TW.StatGRs[2] = MonGR_Intelligence;
                        TW.StatGRs[3] = MonGR_Skill;
                        TW.StatGRs[4] = MonGR_Speed;
                        TW.StatGRs[5] = MonGR_Defence;
                        TW.LifeStage = Mon_LifeStage;
                        TW.PopulateSpecialty();
                    }
                    if (MMW != null)
                    {
                        MMW.Mon_Moves = Mon_Moves;
                        MMW.Mon_MoveUsed = Mon_MoveUsed;
                        MMW.Mon_Genus = MonGenus_Main;
                        MMW.Mon_SubGenus = MonGenus_Sub; // GREATEST PATCH OF THE CENTUARY
                        MMW.Mon_Nature = Mon_EffNature; // send effnature too //bedeg
                        MMW.Mon_Stats[0] = Mon_Lif;
                        MMW.Mon_Stats[1] = Mon_Pow;
                        MMW.Mon_Stats[2] = Mon_Int;
                        MMW.Mon_Stats[3] = Mon_Skl;
                        MMW.Mon_Stats[4] = Mon_Spd;
                        MMW.Mon_Stats[5] = Mon_Def;
                        MMW.MonActMoves = ActiveMoves;
                        CollateMonMoves();
                    }
                    ListItems();
                    if (il != null)
                    {
                        il.itemIDs = ItemList;
                        il.ItemListUpdate();
                    }
                    if (mDBG != null)
                    {
                        if (mDBG.bReadOK)
                        {
                            for (int d = 0; d < 4; d++)
                            {
                                if (mDBG.AddrINT[d] != -1)
                                {
                                    if (mDBG.DataMode[d] < 2)
                                        mDBG.DataINT[d] = MR2ReadInt(mDBG.AddrINT[d]);
                                    else if (mDBG.DataMode[d] == 2)
                                        mDBG.DataINT[d] = MR2ReadDouble(mDBG.AddrINT[d]);
                                    else if (mDBG.DataMode[d] == 3)
                                        mDBG.DataINT[d] = MR2ReadQuad(mDBG.AddrINT[d]);
                                }
                            }
                        }
                        mDBG.ProcessData();
                    }
                    BananaCount = 0;
                    for (int i = 0; i < 20; i++)
                    {
                        if (MonDesireNames(ItemList[i]) == "Magic Banana")
                            BananaCount++;
                    }
                    if (BananaTicks > 0)
                    {
                        BananaTicks--;
                        if (BananaTicks <= 1 && MonLifespanBox.BackColor != SystemColors.Control)
                        {
                            BananaLose.Stop();
                            BananaWin.Stop();
                            MonLifespanBox.BackColor = SystemColors.Control;
                        }
                    }

                    if (MonBanaScumToggle.Checked && (OldBananaCount - BananaCount == 1))
                    {
                        BananaTicks = Convert.ToInt32(1000.0 / MainTime.Interval);
                        Console.Write("BananaTicks = " + BananaTicks);
                        if ((Mon_Lifespan == Mon_OldLifespan - 1) && (Mon_LoyalSpoil > Mon_OldLoyalSpoil || Mon_OldLoyalSpoil == 100) && (Mon_LoyalFear > Mon_OldLoyalFear || Mon_LoyalFear == 100))
                        {
                            BananaLose.Play(); // Magic Banana scum failure plays a sad noise.
                            MonLifespanBox.BackColor = Color.IndianRed;
                        }
                        else if ((Mon_Lifespan == Mon_OldLifespan + 1) && (Mon_LoyalSpoil < Mon_OldLoyalSpoil || Mon_OldLoyalSpoil == 0) && (Mon_LoyalFear < Mon_OldLoyalFear || Mon_LoyalFear == 0))
                        {
                            BananaWin.Play(); // Magic Banana scum success plays a chime.
                            MonLifespanBox.BackColor = Color.LightGreen;
                        }
                        else if ((Mon_LoyalSpoil > Mon_OldLoyalSpoil || Mon_OldLoyalSpoil == 100) && (Mon_LoyalFear < Mon_OldLoyalFear || Mon_LoyalFear == 0))
                        {
                            // Conversion of Spoil to Fear doesn't ring a bell.
                            MonLifespanBox.BackColor = Color.LightYellow;
                        }
                    }
                    Mon_OldLifespan = Mon_Lifespan;
                    Mon_OldLoyalFear = Mon_LoyalFear;
                    Mon_OldLoyalSpoil = Mon_LoyalSpoil;
                    OldBananaCount = BananaCount;
                }
            }
        }

        private void Label4_Click(object sender, EventArgs e)
        {
            AgeWeeksOnly = !AgeWeeksOnly;
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (cv != null)
            {
                ReadInput();
                for (int i = 0; i < 16; i++)
                {
                    cv.P1_Control[i] = Con1Input[i];
                }
                cv.UpdateConDisplay();
                cv.P1Raw = P1Input;
            }
        }

        private void EXFeaturesChkBox_CheckedChanged(object sender, EventArgs e)
        {
            CycleFeatureDisplay();
        }

        private void LIWButton_Click(object sender, EventArgs e)
        {
            if (LIW == null)
            {
                LIW = new MonLifeIndexWindow();
                LIW.FormClosing += FManage_LIWClose;
                LIW.Show();
                LIW.Fatigue = Mon_Fatigue;
                LIW.Stress = Mon_Stress;
                LIW.LIBox = MonLifeIndexBox.Text;
                LIW.LSR = Mon_Lifespan;
                LIW.LSM = Mon_InitLifespan;
                LIW.boxCol = MonLifeIndexBox.BackColor;
            }
        }

        private void MVButton_Click(object sender, EventArgs e)
        {
            if (MMW == null)
            {
                MMW = new MonMoveWindow();
                MMW.FormClosing += FManage_MMWClose;
                MMW.Show();
            }
        }

        private void TWButton_Click(object sender, EventArgs e)
        {
            if (TW == null)
            {
                TW = new TrainingWindow();
                TW.FormClosing += FManage_TWClose;
                TW.Show();
            }
        }

        private void IVButton_Click(object sender, EventArgs e)
        {
            if (cv == null)
            {
                cv = new MR2ControllerView();
                cv.FormClosing += FManage_cvClose;
                cv.Show();
            }
        }

        private void ErrSaleBox_CheckedChanged(object sender, EventArgs e)
        {
            IsErrantrySaleReady();
        }

        private void IsErrantrySaleReady()
        {
            ErrSaleBox.Checked = (Game_ErrantryCD == 255);
        }

        private void MonItemUsedBox_CheckedChanged(object sender, EventArgs e)
        {
            MonItemUsedBox.Checked = Mon_ItemUsed;
        }

        private void MonSilverPeachBox_CheckedChanged(object sender, EventArgs e)
        {
            MonSilverPeachBox.Checked = MonPeach_Silver;
        }

        private void MonGoldPeachBox_CheckedChanged(object sender, EventArgs e)
        {
            MonGoldPeachBox.Checked = MonPeach_Gold;
        }

        private void UpdateRate_Scroll(object sender, EventArgs e)
        {
            MainTime.Interval = UpdateRate.Value * 125; // specify interval time as you want
            float fTickRate = Convert.ToSingle(1000.0 / MainTime.Interval);
            TickDisplay.Text = fTickRate.ToString("0.0");
            if (UpdateRate.Value > 4)
            {
                MonBanaScumToggle.Checked = false;
                MonBanaScumToggle.Enabled = false;
            }
            else
            {
                MonBanaScumToggle.Enabled = true;
            }
        }

        private void ItemViewButton_Click(object sender, EventArgs e)
        {
            if (il == null)
            {
                il = new ItemLister
                {
                    AVW = this
                };
                il.FormClosing += FManage_ilClose;
                il.Show();
            }
        }

        private void MonCocoonReady_CheckedChanged(object sender, EventArgs e)
        {
            if ((Mon_Age >= 192) && ((Mon_LoyalSpoil / 2 + Mon_LoyalFear / 2) >= 80) && Mon_Fatigue == 0 && Mon_Stress < 30)
                MonCocoonReady.Checked = true;
            else
                MonCocoonReady.Checked = false;
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void MonNatureBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void MRDebugButton_Click(object sender, EventArgs e)
        {
            if (mDBG == null)
            {
                mDBG = new MRDebug
                {
                    AVW = this
                };
                mDBG.FormClosing += FManage_mDBGClose;
                mDBG.Show();
            }
        }

        private void CycleFeatureDisplay()
        {
            bShowingExtras = !bShowingExtras;
            if (bShowingExtras)
            {
                Width = 740;
                TWButton.Show();
                LIWButton.Show();
                MVButton.Show();
                MRDebugButton.Show();
                ItemViewButton.Show();
            }
            else
            {
                Width = 640;
                TWButton.Hide();
                LIWButton.Hide();
                MVButton.Hide();
                MRDebugButton.Hide();
                ItemViewButton.Hide();
            }
        }

        private void ToolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void StatusBarURL_Click(object sender, EventArgs e)
        {
            switch (RNGCur)
            {
                case 1:
                    Process.Start("https://twitch.tv/lexichu_");
                    break;
                case 2:
                    Process.Start("https://legendcup.com/");
                    break;
                case 3:
                    Process.Start("https://streamelements.com/lexichu_/tip");
                    break;
                case 4:
                    Process.Start("https://w.atwiki.jp/mf2_matome/");
                    break;
                default:
                    Process.Start("https://twitch.tv/lexichu_");
                    break;
            }
        }

        private void DumpGrowths_Click(object sender, EventArgs e)
        {
            string OutputText;

            OutputText = "Gro: " + MonGRAlphabetise(MonGR_LIF, MoneyBox) + "/" + MonGRAlphabetise(MonGR_Power, MoneyBox) + "/" + MonGRAlphabetise(MonGR_Intelligence, MoneyBox) + "/" + MonGRAlphabetise(MonGR_Skill, MoneyBox) + "/" + MonGRAlphabetise(MonGR_Speed, MoneyBox) + "/" + MonGRAlphabetise(MonGR_Defence, MoneyBox) + "; GR:" + Mon_GutsRate + "; Nat:" + Mon_NatureBase + "; Wks:" + Mon_InitLifespan + ";  Spc: " + MonReadBattleSpecials();
            System.Windows.Forms.Clipboard.SetText(OutputText);
            MoneyBox.BackColor = SystemColors.Control;
            MessageBox.Show(OutputText, "Copied Growths to clipboard!");
        }

        private void MonGivenNameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void MoneyBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void ChangeName_Click(object sender, EventArgs e) //bedeg
        {
            bChangingName = true;
            ChangeName.Visible = false;
            ChangeName.Location = new Point(69, 245);
            SaveName.Visible = true;
            SaveName.Location = new Point(26, 245);
            CancelName.Visible = true;
            CancelName.Location = new Point(116, 245);

            prevName = MonGivenNameBox.Text;

            MonGivenNameBox.ReadOnly = false;
            MonGivenNameBox.Focus();
        }

        private void SaveName_Click(object sender, EventArgs e) //bedeg
        {
            if (MonGivenNameBox.Text == "")
                MonGivenNameBox.Text = prevName;

            convTextAndWrite();

            bChangingName = false;
            ChangeName.Visible = true;
            SaveName.Visible = false;
            CancelName.Visible = false;
            MonGivenNameBox.ReadOnly = true;
        }

        private void CancelName_Click(object sender, EventArgs e) //bedeg
        {
            MonGivenNameBox.Text = prevName;

            bChangingName = false;
            ChangeName.Visible = true;
            SaveName.Visible = false;
            CancelName.Visible = false;
            MonGivenNameBox.ReadOnly = true;
        }

        private void convTextAndWrite() //bedeg
        {
            int offsetIncrement = 0;
            Array.Clear(nameToWrite, 0, 24);

            foreach (char c in MonGivenNameBox.Text)
            {
                string r = Convert.ToString(c);
                if (ReverseCharMapping.reverseCharMap.ContainsKey(r))
                {
                    if (ReverseCharMapping.reverseCharMap[r] <= 0xab)                                                       // 1 byte
                    {
                        nameToWrite[offsetIncrement] = BitConverter.GetBytes(ReverseCharMapping.reverseCharMap[r])[0];
                        offsetIncrement += 1;
                    }
                    else                                                                                                    // 2 bytes
                    {
                        nameToWrite[offsetIncrement] = BitConverter.GetBytes(ReverseCharMapping.reverseCharMap[r])[1];      // endianness madness
                        offsetIncrement += 1;
                        nameToWrite[offsetIncrement] = BitConverter.GetBytes(ReverseCharMapping.reverseCharMap[r])[0];
                        offsetIncrement += 1;
                    }
                }
                else
                {
                    nameToWrite[offsetIncrement] = 0xb0;                                                                    // write "?" if character is not found
                    offsetIncrement += 1;
                    nameToWrite[offsetIncrement] = 0x37;
                    offsetIncrement += 1;
                }
            }
            if (offsetIncrement < 24)                                                                                       // append FFFF to the end
            {
                for (int i = offsetIncrement; i < 24; i++)
                    nameToWrite[i] = 0xFF;
            }
            WriteProcessMemory(psxPTR, PSXBase + 0x0097B78, nameToWrite, 24, ref HasRead);
        }

        private void MR2Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(EmuVer != DXSelectionID)
            {
                bJPNMode = (MR2Mode.SelectedIndex == 2);
            }
        }

        private void StatusMessageCycle_Tick(object sender, EventArgs e)
        {
            StatusBarMSG.Text = CycleMessages();
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            RawNatureMod = !RawNatureMod;
            if (RawNatureMod)
                Label2.Text = "Nat. Shift";
            else
                Label2.Text = "Eff. Nature";
        }

        private void CocoonInfo_Click(object sender, EventArgs e)
        {
            string InfoText =
@"Cocooning requires a Worm that has:
- Zero fatigue. ('[Monster] is very well!')
- Less than 30 Stress.
- *80+ Loyalty.
- C Rank or lower.
- Lived for at least 4 years, and is on the Ranch when the calender reaches 'June, Week 4.'

If the 'Cup Jellies Eaten' counter is green, you have fed enough Cup Jellies for your Worm to become a Beaclon.
If the box is not green, but the 'Cocoon Ready' box is ticked, your Worm will cocoon into a random /Worm monster.

 If this cocooning information disappears, your Worm is past C Rank, therefore it cannot cocoon any more.

*
79 Loyalty is possible, with one of Spoil/Fear being 79 and the other 80.
78 Loyalty is possible with both Spoil and Fear being 79.
'Can Cocoon' tickbox will only activate on 80+ Loyalty, despite these two cases.";
            MessageBox.Show(InfoText, "Cocooning Information:", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void FManage_MMWClose(object sender, FormClosingEventArgs e) => MMW = null;
        public void FManage_LIWClose(object sender, FormClosingEventArgs e) => LIW = null;
        public void FManage_cvClose(object sender, FormClosingEventArgs e) => cv = null;
        public void FManage_TWClose(object sender, FormClosingEventArgs e) => TW = null;
        public void FManage_ilClose(object sender, FormClosingEventArgs e) => il = null;
        public void FManage_mDBGClose(object sender, FormClosingEventArgs e) => mDBG = null;

        private void MonBanaScumToggle_CheckedChanged(object sender, EventArgs e)
        {
            BananaTicks = 0;
            if (EmuVer == DXSelectionID && MonBanaScumToggle.Checked == true)
            {
                MessageBox.Show("Banana Chimes are not currently enabled for MR2DX.");
                MonBanaScumToggle.Checked = false;
            }
        }
    }
}
