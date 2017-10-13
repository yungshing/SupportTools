
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
namespace SupportTools
{
    public class FTPDownload 
    {

        public Exception E
        {
            get
            {
                return e;
            }
        }
        private Exception e;
        public FTPDownload(string username, string password)
        {
            userName = username;
            this.password = password;
        }
        public FTPDownload()
        {

        }
        private string userName, password;
        private bool pause = false;
        public bool Pause
        {
            get { return pause; }
            set { pause = value; }
        }
        public float DownloadSpeed { get; protected set; }
        private FtpWebRequest request = null;
        private FtpWebResponse response;
        private Stream getStream;
        FileStream wrStream;
        private void ConnectFTP(string url)
        {
            ShowDownloadSpeed("建立通信中..");
            request = (FtpWebRequest)WebRequest.Create(new Uri(url));
            request.KeepAlive = false;
            request.UseBinary = true;
            request.Timeout = 15000;
            if (userName == null || userName.Replace(" ", "") == "")
            {
                request.Credentials = new NetworkCredential();
            }
            else
            {
                request.Credentials = new NetworkCredential(userName, password);
            }
            request.Method = WebRequestMethods.Ftp.DownloadFile;
        }
        /// <summary>
        /// 路径要带后缀名
        /// </summary>
        /// <param name="downloadPath"></param>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public bool Download(string downloadPath, string savePath)
        {
            try
            {
                while (Pause)
                {
                    continue;
                }
                SetProgressBar(0, 1);
                ShowPercent("0%");
                SetProgressBar(0, 1);
                ShowPercent("0%");
                SetProgressBar(0, 1);
                ShowPercent("0%");
                ConnectFTP(downloadPath);
                long downloadBytes = 0;
                long TotalBytes = 0;
                string _path_Tmp = savePath + ".zzfz";
                if (File.Exists(savePath))
                {
                    SetProgressBar(1, 1);
                    ShowRemainTime("0");
                    ShowPercent("100%");
                    ShowDownloadSpeed("已下载");
                    return true;
                }
                ///断点续传
                if (File.Exists(_path_Tmp))
                {
                    wrStream = File.OpenWrite(_path_Tmp);
                    downloadBytes = wrStream.Length;
                    wrStream.Seek(downloadBytes, SeekOrigin.Current);
                    request.ContentOffset = downloadBytes;
                    TotalBytes = Size(downloadPath);
                    SetProgressBar((int)downloadBytes, (int)TotalBytes);
                    if (wrStream.Length == TotalBytes)
                    {
                        Dispose();
                        File.Move(_path_Tmp, savePath);
                        return true;
                    }
                }
                else
                {
                    wrStream = new FileStream(_path_Tmp, FileMode.Create, FileAccess.ReadWrite);
                    TotalBytes = Size(downloadPath);
                }
                Stopwatch stopWatch = new Stopwatch();
                ShowDownloadSpeed("接收数据中..");
                response = (FtpWebResponse)request.GetResponse();
                getStream = response.GetResponseStream();
                int RemainTime = 0;
                int n = 1;
                var bytes = new byte[102400];
                stopWatch.Reset();
                stopWatch.Start();
                int speed_tmp = 0;
                while (n > 0)
                {
                    if (Pause) continue;
                    n = getStream.Read(bytes, 0, bytes.Length);
                    downloadBytes += n;
                    SetProgressBar((int)downloadBytes, (int)TotalBytes);
                    var f = (float)downloadBytes / (float)TotalBytes;
                    f = f * 100f;
                    ShowPercent(((int)f).ToString() + "%");
                    speed_tmp += n;
                    if (stopWatch.ElapsedMilliseconds >= 800)
                    {
                        stopWatch.Stop();
                        DownloadSpeed = speed_tmp * 1000f / stopWatch.ElapsedMilliseconds;
                        ShowDownloadSpeed(DownloadSpeed.ToString());
                        RemainTime = (int)((TotalBytes - downloadBytes) / DownloadSpeed);
                        ShowRemainTime(RemainTime.ToString());
                        speed_tmp = 0;
                        stopWatch.Reset();
                        stopWatch.Start();
                    }
                    wrStream.Write(bytes, 0, n);
                }
                getStream.Dispose();
                wrStream.Dispose();
                File.Move(_path_Tmp, savePath);
                Dispose();
                return true;
            }
            catch (Exception e)
            {
                this.e = e;
                return false;
            }
            finally
            {
                Dispose();
            }
        }
        void SetProgressBar(int c, int max)
        {
        }


        private void ShowRemainTime(string s)
        {
           
        }
        void ShowDownloadSpeed(string speed)
        {
        }
        private void ShowPercent(string s)
        {
           
        }
 


        public long Size(string downloadPath)
        {
            FtpWebResponse res = null;
            FtpWebRequest r = null;
            long size = 0;
            try
            {
                r = (FtpWebRequest)WebRequest.Create(new Uri(downloadPath));
                r.KeepAlive = false;
                r.Method = WebRequestMethods.Ftp.GetFileSize;
                r.Timeout = 5000;
                r.Credentials = new NetworkCredential(userName, password);
                using (res = (FtpWebResponse)r.GetResponse())
                {
                    using (var sm = res.GetResponseStream())
                    {
                        size = res.ContentLength;
                    }
                }
            }
            catch
            {
                size = -1;
            }
            finally
            {
                if (r != null)
                {
                    r.Abort();
                }
                if (res != null)
                {
                    res.Close();
                }
            }
            return size;
        }
        public void Dispose()
        {
            try { wrStream.Dispose(); }
            catch { }
            try { getStream.Dispose(); }
            catch { }
            try { response.Close(); }
            catch { }
            try { request.Abort(); }
            catch { }
        }
 
    }
}