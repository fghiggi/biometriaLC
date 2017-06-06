using System;
using System.Runtime.InteropServices;
    public class ASO15_DEF
    {
        public const int RES_OK = 0;
        public const int IS_EMPTY_ID = 1;
        public const int IS_EMPTY_FINGER = 1;
        public const int IS_MANAGER = 1;
        public const int IS_USER = (0);

        public const int ERR_MEMORY = (-1);
        public const int ERR_INVALID_ID = (-2);
        public const int ERR_INVALID_TEMPLATE = (-3);
        public const int ERR_NOT_EMPTY = (-4);
        public const int ERR_DISK_SIZE = (-5);
        public const int ERR_NOT_USBDEV = (-6);
        public const int ERR_INVALID_PARAMETER = (-7);
        public const int ERR_FAIL_INIT_ENGINE = (-8);
        public const int ERR_FAIL_MATCH_PROC = (-9);
        public const int ERR_FP_TIMEOUT = (-10);
        public const int ERR_BAD_QUALITY = (-11);
        public const int ERR_FAIL_GEN = (-12);
        public const int ERR_FAIL_FILE_IO = (-13);
        public const int ERR_ENROLLED_ID = (-14);
        public const int ERR_ENROLLED_FINGER = (-15);
        public const int ERR_INVALID_FINGERNUM = (-16);
        public const int ERR_FAIL_MATCH = (-17);
        public const int ERR_NOT_ENROLLED = (-18);
        public const int ERR_FAIL_SET = (-19);
        public const int ERR_DUPLICATED = (-20);
        public const int ERR_CANCELED = (-21);
        public const int ERR_UNKNOWN = (-30);

        //. Const
        public const int SFEPDB_REG_MAX = (100000);
        public const int MAX_IDNUMBER = (SFEPDB_REG_MAX);
        public const int MAX_FPNUMBER = (10);

        public const int SFEP_UFPDATA_SIZE = (498);

        public const int IMAGE_WIDTH = (600);
        public const int IMAGE_HEIGHT = (400);

        public const int IMAGE_FULL_WIDTH = (636);
        public const int IMAGE_FULL_HEIGHT = (478);

        //. Struct
        public struct SFEP_USER_FPDATA
        {
            public byte[] rbData;
        };

        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_Initialize();
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_Uninitialize();
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_SetDatabasePath(string szPath);
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_SetConfig(IntPtr hWnd);
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_SetBrightness(byte bBVal);
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_GetBrightness(ref byte pbBVal);
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_GetLiveImage();        
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_CalcBrightness();
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_CalcBrightnessInFullImage();
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_CurrentSaveBMP(string szFileName, Int32 nWidth, Int32 nHeight);
        [DllImport("SPL_ASO15.dll")]
        public static extern bool SFEP_IsFingerPress();
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_CaptureFingerImage(UInt32 dwTimeOut);
        [DllImport("SPL_ASO15.dll")]
        public static extern void SFEP_FpCancel();
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_CreateTemplate(ref byte pTemplate);
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_GetTemplateForRegister(ref byte pTemplates, ref byte pRegTem);
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_Enroll(ref byte pTemplate, ref UInt32 pdwID, ref byte pbFingerNum, ref byte pbManager);
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_Identify(ref byte pTemplate, ref UInt32 pdwID, ref byte pbFingerNum, ref byte pbManager, byte bSecLevel);
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_Verify(ref byte pTemplate, UInt32 dwID, byte bFingerNum, byte bSecLevel);
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_Match2Template(ref byte pTemplate1, ref byte pTemplate2, byte bSecLevel);
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_RemoveTemplate(UInt32 dwID, byte bFingerNum);
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_RemoveAll();
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_ReadTemplate(UInt32 dwID, byte bFingerNum, string szFileName);
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_WriteTemplate(UInt32 dwID, byte bFingerNum, byte bManager, string szFileName);
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_GetEnrollCount();
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_CheckID(UInt32 dwID);
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_CheckFingerNum(UInt32 dwID, byte bFingerNum);
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_SearchID(ref UInt32 pdwID);
        [DllImport("SPL_ASO15.dll")]
        public static extern Int32 SFEP_SearchFingerNumber(UInt32 pdwID, ref byte pbFingerNum);

    };
