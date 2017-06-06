using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopDataBiometriaLC.classes
{
    public class Biometria
    {
        private const int QUANTIDADE_CAPTURA_DIGITAL = 3;
        private const int TIMEOUT_CAPTURA = 10;
        public int quantidadeCaptura { get; set; }
        private byte[] _template;
        private IntPtr _handle;

        public Biometria(IntPtr handle)
        {
            _template = new byte[ASO15_DEF.SFEP_UFPDATA_SIZE * QUANTIDADE_CAPTURA_DIGITAL];
            _handle = handle;
        }

        public void Capturar()
        {
            try
            {
                InformarCaminhoDatabaseDigitais();

                IniciarComunicacaoScanner();

                ConfigurarHandlerImagem();

                ConfigurarBrilhoHamster();

                EfetuarCapturaDigital();

                CriarTemplate();

                RecuperarUltimaImagemCapturada();

                quantidadeCaptura += 1;

                while (quantidadeCaptura != QUANTIDADE_CAPTURA_DIGITAL && ASO15_DEF.SFEP_IsFingerPress())
                {
                    Thread.Sleep(100);
                }

                if (quantidadeCaptura == QUANTIDADE_CAPTURA_DIGITAL)
                {
                    ObterTemplateParaSalvar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                FinalizarComunicacaoScanner();
            }
        }

        private void InformarCaminhoDatabaseDigitais()
        {
            int resposta = ASO15_DEF.SFEP_SetDatabasePath(AppDomain.CurrentDomain.BaseDirectory);

            if (resposta != ASO15_DEF.RES_OK)
            {
                throw new Exception(resposta.ToString());
            }
        }

        private void IniciarComunicacaoScanner()
        {
            int resposta = ASO15_DEF.SFEP_Initialize();

            if (resposta != ASO15_DEF.RES_OK)
            {
                throw new Exception(resposta.ToString());
            }
        }

        private void ConfigurarHandlerImagem()
        {
            int resposta = ASO15_DEF.SFEP_SetConfig(_handle);

            if (resposta != ASO15_DEF.RES_OK)
            {
                throw new Exception(resposta.ToString());
            }
        }

        private void ConfigurarBrilhoHamster()
        {
            const byte BRILHO_PADRAO = 130;
            byte brilhoHamster = 0;
            //Valor original 103

            if (ASO15_DEF.SFEP_GetBrightness(ref brilhoHamster) != ASO15_DEF.RES_OK)
            {
                throw new Exception("Falha ao obter valor do brilho");
            }

            if (brilhoHamster == BRILHO_PADRAO)
            {
                return;
            }

            if (ASO15_DEF.SFEP_SetBrightness(BRILHO_PADRAO) != ASO15_DEF.RES_OK)
            {
                throw new Exception("Falha configurar valor do brilho");
            }
        }

        private void EfetuarCapturaDigital()
        {
            Console.WriteLine("Coloque o dedo no leitor");
            int resposta = ASO15_DEF.SFEP_CaptureFingerImage(TIMEOUT_CAPTURA);

            if (resposta != ASO15_DEF.RES_OK)
            {
                throw new Exception(resposta.ToString());
            }
        }

        private void CriarTemplate()
        {
            int resposta = ASO15_DEF.SFEP_CreateTemplate(ref _template[quantidadeCaptura * ASO15_DEF.SFEP_UFPDATA_SIZE]);

            if (resposta != ASO15_DEF.RES_OK)
            {
                throw new Exception(resposta.ToString());
            }
        }

        private void RecuperarUltimaImagemCapturada()
        {
            string caminhoImagem = Path.GetTempFileName();

            int resposta = ASO15_DEF.SFEP_CurrentSaveBMP(caminhoImagem, ASO15_DEF.IMAGE_WIDTH, ASO15_DEF.IMAGE_HEIGHT);

            if (resposta != ASO15_DEF.RES_OK)
            {
                throw new Exception(resposta.ToString());
            }

            Console.WriteLine("Nova template obtida");
        }

        private byte[] ObterTemplateParaSalvar()
        {
            byte[] templateData = new byte[ASO15_DEF.SFEP_UFPDATA_SIZE];

            int resposta = ASO15_DEF.SFEP_GetTemplateForRegister(ref _template[0], ref templateData[0]);

            if (resposta != ASO15_DEF.RES_OK)
            {
                throw new Exception(resposta.ToString());
            }

            return templateData;
        }

        private void FinalizarComunicacaoScanner()
        {
            ASO15_DEF.SFEP_Uninitialize();
        }
    }
}
