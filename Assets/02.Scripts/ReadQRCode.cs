using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ReadQRCode : MonoBehaviour
{
    public ARCameraManager CameraManager;
    public Text txt;


    private void Update()
    {
        if (CameraManager.TryAcquireLatestCpuImage(out XRCpuImage image)) //ī�޶󿡼� �޾ƿ� ���� �ֽ��� �̹����� ������ ��
        {
            using (image)
            {
                //8bit image�� ��ȯ,  y���� �������� ������ ��.  (ī�޶� ���� �¿� ����)
                var conversionParams = new XRCpuImage.ConversionParams(image, TextureFormat.R8, XRCpuImage.Transformation.MirrorY);

                var dataSize = image.GetConvertedDataSize(conversionParams);
                var grayscalePixel = new byte[dataSize];

                unsafe
                {
                    fixed (void* ptr = grayscalePixel)
                    {
                        image.Convert(conversionParams, new System.IntPtr(ptr), dataSize);

                    }
                }

                IBarcodeReader barcodeReader = new BarcodeReader();
                var result = barcodeReader.Decode(grayscalePixel, image.width, image.height, RGBLuminanceSource.BitmapFormat.Gray8);
                if (result != null)
                {
                    txt.text = result.Text;
                    QRObjectPlacement.Instance.qrcode = result.Text; // put the value to the singleton.
                }

            }
        }
    }
}
