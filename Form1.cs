using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace UseJavaScriptCSharp
{
    [ComVisible(true)]
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
            this.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);

            //If you want to call the C# code (method) in java script function then write this code.
            webBrowser1.ObjectForScripting = this;
            webBrowser1.ScriptErrorsSuppressed = false;

            // if you want to disable right click on web browser control then write this code
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
            webBrowser1.AllowWebBrowserDrop = false;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // when the form is load cursor focus on the web browser control
            webBrowser1.Focus();

            // Here I have to call report method which contain the report content
            Report();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // In below written code you have to get the current directory of this application
            string CurrentDirectory = Directory.GetCurrentDirectory();
            // Here you have to call HTML page using navigate method. Its mandetory to call navigate method when you have to fire web
            // Browser document completed event.
            webBrowser1.Navigate(Path.Combine(CurrentDirectory, "HTMLPageForJavaScript.html"));
        }

        private void Report()
        {
            //Here I have to get HTML page div from id of Div.
            HtmlElement div = webBrowser1.Document.GetElementById("reportContent");

            //here create a simple html content
            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");
            sb.Append("<tr><td><B> Hi this is my report demo</B></td></tr>");
            sb.Append("</table>");

            //here I have to assign content to the html page div which is display on browser control
            div.InnerHtml = sb.ToString();
        }

        public void PrintReport()
        {
            // I am simply show print dialog and call print method of webbrowser control.
            DialogResult dr = printDialog1.ShowDialog();

            if (dr.ToString() == "OK")
            {
                webBrowser1.Print();
            }
            else
            {
                return;
            }
        }
    }
}
