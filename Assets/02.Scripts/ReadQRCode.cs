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
        if (CameraManager.TryAcquireLatestCpuImage(out XRCpuImage image)) //카메라에서 받아온 가장 최신의 이미지를 가지고 옴
        {
            using (image)
            {
                //8bit image로 변환,  y축을 기점으로 뒤집어 줌.  (카메라 상이 좌우 반전)
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
