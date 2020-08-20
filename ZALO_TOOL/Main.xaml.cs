using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZALO_TOOL.Entity;
using ZaloDotNetSDK;


namespace ZALO_TOOL
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private ZaloAppInfo appInfo;
        private ZaloAppClient appClient;
        private string code = "";
        private string access_token = "";
        private string id_friend_to_send = "";
        private string name_friend_to_send = "";

        public Main()
        {
            InitializeComponent();
        }
        string url_CallBack = "https://77724647e5fd.ngrok.io";
        //"https://id.zalo.me/account?continue=https%3A%2F%2Fchat.zalo.me%2F"
        private void btnAccount_Add_Click(object sender, RoutedEventArgs e)
        {
            appInfo = new ZaloAppInfo(2560572527426193282, "ks69Nt8JOgLOIrrM57DC", url_CallBack);
            appClient = new ZaloAppClient(appInfo);
            string loginUrl = appClient.getLoginUrl();
            string loginUrl_inc = "--incognito " + loginUrl;
            Process.Start(@"chrome.exe", loginUrl_inc);


            //string code = "uSB1rGJGL2FbuApaCiTQDgIQele7dbKzu9JI-028O4dKhUUA1f0KJz71vhT-fXbvrwBklJ6XN5BglTRXDfrQ9UQkg-SyZcqrhTgagIZaNtF7xzQIDivUEk3ecEyGyHHmY8c7xdVoUppMu9oVORr7FzRUtFTdwLWohTsYuc_SGYR5iwkYOhLDTltsWjLOiLmnzi-TwoVuQpNstxJl8yCf3_-zojjDvnvnhR30b2F8N464fE_U2z0_5A6gniO4k7SuzBhxmXJv8IAGdFrtGfWzfzQ8yb5es7kb-UAKLYIV5QUxhwWXNd0vKevq-A9iH5OGfJFQo6OaGMBXCQk7Q0bVADrFgeeQ3ayetKlkWoOFfML19TbwKm";
            //string token = appClient.getAccessToken(code).ToString();

            //JObject profile = appClient.getProfile(token, "id,name,gender,picture");

            
            
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAccount_Get_Token_Click(object sender, RoutedEventArgs e)
        {
            
            Process[] procsChrome = Process.GetProcessesByName("chrome");

            foreach (Process chrome in procsChrome)
            {
                if (chrome.MainWindowHandle == IntPtr.Zero)
                    continue;

                AutomationElement elm = AutomationElement.FromHandle(chrome.MainWindowHandle);
                AutomationElement elmUrlBar = elm.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ToolBar));

                AutomationElementCollection elm1 = elm.FindAll(TreeScope.Subtree, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));
                AutomationElement elm3 = elm1[0];
                string vp = ((ValuePattern)elm3.GetCurrentPattern(ValuePattern.Pattern)).Current.Value as string;
                if (vp.Contains("code")) {

                    string[] v_arrCode_1 = vp.Split(new string[] { "&code=" }, StringSplitOptions.None);
                    string code_temp = v_arrCode_1[1];
                    string[] v_arrCode_2 = code_temp.Split(new string[] { "&scope" }, StringSplitOptions.None);
                    code = v_arrCode_2[0];
                }
            }
            JObject accessToken = appClient.getAccessToken(code);
            access_token = accessToken["access_token"].ToString();

            JObject Profile = appClient.getProfile(access_token, "name");
            profile v_objProfile = new profile();
            v_objProfile.name = Profile["name"].ToString();
            IList<profile> v_arrProfile = new List<profile>();
            v_arrProfile.Add(v_objProfile);

            dgAccount.ItemsSource = v_arrProfile;
        }
    }
}
