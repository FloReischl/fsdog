// Decompiled with JetBrains decompiler
// Type: FR.Drawing.CommonImages
// Assembly: FR.Drawing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a1c377fe888b0e9a
// MVID: 5443DC0B-C77E-46BB-B960-A3DBDF862D86
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Drawing.dll

using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FR.Drawing
{
  public static class CommonImages
  {
    private static Hashtable _embeddedData;
    private static object _embeddedNoData;
    private static ImageList _iml;

    public static Image GetImage(CommonImageType imageType)
    {
      Stream embeddedStream = CommonImages.GetEmbeddedStream(string.Format("{0}.Images.{1}.png", (object) typeof (CommonImages).Namespace, (object) imageType.ToString()));
      return embeddedStream == null ? (Image) null : Image.FromStream(embeddedStream);
    }

    public static ImageList GetImageList(bool globalInstance)
    {
      if (globalInstance && CommonImages._iml != null)
        return CommonImages._iml;
      ImageList imageList = new ImageList();
      imageList.Images.Add(CommonImageType.Unknown.ToString(), CommonImages.GetImage(CommonImageType.Unknown));
      foreach (CommonImageType imageType in Enum.GetValues(typeof (CommonImageType)))
      {
        if (imageType != CommonImageType.Unknown)
          imageList.Images.Add(imageType.ToString(), CommonImages.GetImage(imageType));
      }
      if (globalInstance)
        CommonImages._iml = imageList;
      return imageList;
    }

    private static Stream GetEmbeddedStream(string resourceName)
    {
      if (CommonImages._embeddedData == null)
      {
        CommonImages._embeddedData = new Hashtable();
        CommonImages._embeddedNoData = new object();
      }
      object obj = CommonImages._embeddedData[(object) resourceName];
      if (obj != null)
      {
        if (obj.Equals(CommonImages._embeddedNoData))
          return (Stream) null;
        Stream embeddedStream = (Stream) obj;
        embeddedStream.Position = 0L;
        return embeddedStream;
      }
      Stream manifestResourceStream = typeof (CommonImages).Assembly.GetManifestResourceStream(resourceName);
      if (manifestResourceStream != null)
        CommonImages._embeddedData.Add((object) resourceName, (object) manifestResourceStream);
      else
        CommonImages._embeddedData.Add((object) resourceName, CommonImages._embeddedNoData);
      return manifestResourceStream;
    }
  }
}
