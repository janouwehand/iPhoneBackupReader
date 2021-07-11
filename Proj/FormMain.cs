using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace iPhoneBackupReader
{
  public partial class FormMain : Form
  {
    private string folderSource, manifestFile, folderDestination;
    private string[] extensions;

    public FormMain() => InitializeComponent();

    protected override void OnShown(EventArgs e)
    {
      folderSource = ConfigurationManager.AppSettings["BackupFolder"].TrimEnd('\\') + '\\';
      manifestFile = string.Concat(folderSource, "Manifest.db");
      folderDestination = ConfigurationManager.AppSettings["DestinationFolder"].TrimEnd('\\') + '\\';
      extensions = ConfigurationManager.AppSettings["FindFileExtensions"].Split(',');

      edt.AppendText($@"Source folder: {folderSource}
Destination folder: {folderDestination}
Manifest file: {manifestFile}
Find files: {(string.Join(", ", extensions))}");
    }

    private void button1_Click(object sender, EventArgs e)
    {
      using (var db = new SQLiteConnection(manifestFile))
      {
        db.Open();

        ReadFilesFromDB(db);
        ReadFilesFromFolder();
        Execute();
      }
    }

    private class FileRec
    {
      public string ID { get; set; }
      public string FilePathName { get; set; }
    }

    private readonly Dictionary<string, FileRec> filesdb = new Dictionary<string, FileRec>();
    private Dictionary<string, string> filesdisc = new Dictionary<string, string>();

    private void ReadFilesFromDB(SQLiteConnection db)
    {
      using (var cmd = db.CreateCommand())
      {
        cmd.CommandText = @"SELECT * from Files;";
        using (var rdr = cmd.ExecuteReader())
        {
          while (rdr.Read())
          {
            var id = rdr[0].ToString();
            var pth = rdr[2].ToString();
            filesdb.Add(id, new FileRec { ID = id, FilePathName = pth });
          }
        }
      }
    }

    private void ReadFilesFromFolder()
    {
      var files = Directory.GetFiles(folderSource, "*.*", SearchOption.AllDirectories);

      filesdisc = files
        .GroupBy(x => Path.GetFileName(x))
        .ToDictionary(x => x.Key, x => x.First());
    }

    private void Execute()
    {
      foreach (var fn in filesdisc)
      {
        if (!filesdb.TryGetValue(fn.Key, out var rec))
        {
          continue;
        }

        if (rec.FilePathName == null || rec.FilePathName.Length < 5)
        {
          continue;
        }

        var ext = rec.FilePathName.Substring(rec.FilePathName.Length - 4).ToLower();

        if (!extensions.Contains(ext))
        {
          continue;
        }

        CopyFile(fn.Value, rec);
      }
    }

    private void CopyFile(string sourceFile, FileRec rec)
    {
      //Media/DCIM/114APPLE/IMG_4792.JPG

      var destFile = string.Concat(
        folderDestination.TrimEnd('\\'),
        '\\',
        rec.FilePathName.Replace('/', '\\'));

      if (rec.FilePathName.ToLower().Contains("whatsapp"))
      {
        destFile = string.Concat(
          folderDestination.TrimEnd('\\'),
          '\\',
          "WhatsApp\\",
          Path.GetFileName(rec.FilePathName));
      }
      else if (rec.FilePathName.ToLower().Contains("@g.us"))
      {
        destFile = string.Concat(
          folderDestination.TrimEnd('\\'),
          '\\',
          "Google\\",
          Path.GetFileName(rec.FilePathName));
      }

      var destMap = Path.GetDirectoryName(destFile);

      if (!Directory.Exists(destMap))
      {
        Directory.CreateDirectory(destMap);
      }

      File.Copy(sourceFile, destFile);
    }

  }
}
