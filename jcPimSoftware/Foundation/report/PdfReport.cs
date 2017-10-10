using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace jcPimSoftware
{
    class PdfReport_Pim
    {
        private PdfReport_Data data;

        public void Do_Print(string fileName, PdfReport_Data data,bool flag)
        {
            this.data = data;

            Document document = new Document(PageSize.A4, 0, 0, 0, 0);

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));

                writer.ViewerPreferences = (PdfWriter.CenterWindow | PdfWriter.FitWindow | PdfWriter.PageModeUseNone);

                document.Open();

                //ʹ����������
                BaseFont baseFont = BaseFont.CreateFont("C:\\WINDOWS\\FONTS\\SIMKAI.TTF",
                                                        BaseFont.IDENTITY_H,
                                                        BaseFont.NOT_EMBEDDED);

                PdfContentByte cb = writer.DirectContent;


                #region ����ǰ���ո������
                float Xleading = 27.5f;
                float Yleading = 27.5f;
                float Xdelta = 10f;
                float Ydelta = 20f;
                #endregion

                DrawReportHeader(cb, baseFont, Xleading, Yleading, Xdelta, Ydelta);

                DrawReportAbstract(cb, baseFont, Xleading, Yleading, Xdelta, Ydelta);

                DrawReportBody(cb, baseFont, Xleading, Yleading, Xdelta, Ydelta);

                DrawReportParams(document, cb, baseFont, Xleading, Yleading, Xdelta, Ydelta, flag);

                DrawReportFooter(cb, baseFont, Xleading, Yleading, Xdelta, Ydelta);

                document.Close();

            }
            catch (Exception de)
            {
                Console.WriteLine(de.StackTrace);
            }
        }


        private void DrawReportHeader(PdfContentByte cb,
                                      BaseFont bFont,
                                      float Xleading,
                                      float Yleading,
                                      float Xdelta,
                                      float Ydelta)
        {
            #region ���Ʊ���ͷ��
            //���Ʊ�ͷ��������ʱ��
            cb.BeginText();
            cb.SetTextMatrix(Xleading, (842f - Xleading));
            cb.SetFontAndSize(bFont, 15);
            cb.ShowText("Date Time: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            cb.EndText();

            //�����Ϸ���ֱ��
            cb.SetLineWidth(4f);
            cb.MoveTo(Xleading, (842 - Yleading - Ydelta));
            cb.LineTo((595f - Xleading), (842f - Yleading - Ydelta));
            cb.Stroke();

            //���ƽ��·�ϸֱ��
            cb.SetLineWidth(1f);
            cb.MoveTo((Xleading + Xdelta), (842f - Yleading - Ydelta * 2f));
            cb.LineTo((595f / 2f - 70f), (842f - Yleading - Ydelta * 2f));

            cb.MoveTo((595f / 2f + 70), (842f - Yleading - Ydelta * 2f));
            cb.LineTo((595f - Xleading - Xdelta), (842f - Yleading - Ydelta * 2f));
            cb.Stroke();

            //���ƽ��·�ϸֱ���ϵ�����
            cb.BeginText();
            string s = "PIM TEST ABSTRACT";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_CENTER, s, 595 / 2, (842f - Yleading - Ydelta - 25f), 0);
            cb.EndText();
            #endregion
        }

        private void DrawReportAbstract(PdfContentByte cb,
                                        BaseFont bFont,
                                        float Xleading,
                                        float Yleading,
                                        float Xdelta,
                                        float Ydelta)
        {
            string s;
            BaseColor col;

            #region ���Ʊ���ժҪ����
            //���ƴ����
            cb.SetLineWidth(1f);
            cb.Rectangle((Xleading + Xdelta), (842f - Yleading - Ydelta * 3f - 180f), (595f - (Xleading + Xdelta) * 2f), 180f);
            cb.Stroke();

            //��������ڵ�С��
            col = new BaseColor(0, 128, 128);
            cb.SetColorFill(col);
            cb.Rectangle((Xleading + Xdelta * 1.5f), (842f - Yleading - Ydelta * 3f - 175f), (595f - (Xleading + Xdelta * 1.5f) * 2f), 170f);
            cb.Fill();
            cb.Stroke();

            //��С���ϻ���ժҪ���֣�����Description��Model NO.��Serial NO.��Operator
            col = new BaseColor(0, 0, 0);
            cb.SetColorFill(col);
            cb.SetFontAndSize(bFont, 15);

            cb.BeginText();
            s = "Description";
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 2f), (842f - Yleading - Ydelta * 3f - 25f), 0);

            s = "Model NO.";
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 2f), (842f - Yleading - Ydelta * 3f - 65f), 0);

            s = "Serial NO.";
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 2f), (842f - Yleading - Ydelta * 3f - 105f), 0);

            s = "Operator";
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 2f), (842f - Yleading - Ydelta * 3f - 145f), 0);


            s = data.Desc;
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 5.5f), (842f - Yleading - Ydelta * 3f - 40f), 0);

            s = data.Modno;
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 5.5f), (842f - Yleading - Ydelta * 3f - 80f), 0);

            s = data.Serno;
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 5.5f), (842f - Yleading - Ydelta * 3f - 120f), 0);

            s = data.Opeor;
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 5.5f), (842f - Yleading - Ydelta * 3f - 160f), 0);
            cb.EndText();


            //�����м�ϸֱ��
            cb.SetLineWidth(1f);
            cb.MoveTo((Xleading + Xdelta), (842f - Yleading - Ydelta * 4f - 180f));
            cb.LineTo((595f / 2f - 70f), (842f - Yleading - Ydelta * 4f - 180f));

            cb.MoveTo((595f / 2f + 70f), (842f - Yleading - Ydelta * 4f - 180f));
            cb.LineTo((595f - Xleading - Xdelta), (842f - Yleading - Ydelta * 4f - 180f));
            cb.Stroke();

            //�����м�ϸֱ���ϵ�����
            cb.BeginText();
            col = new BaseColor(0, 0, 0);
            cb.SetColorFill(col);
            s = "PIM TEST DETAILS";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_CENTER, s, 595 / 2, (842f - Yleading - Ydelta * 4.3f - 180f), 0);
            cb.EndText();
            #endregion
        }

        private void DrawReportBody(PdfContentByte cb,
                                    BaseFont bFont,
                                     float Xleading,
                                     float Yleading,
                                     float Xdelta,
                                     float Ydelta)
        {

            #region ���Ʊ������岿�֣���Ƕ����ԵĽ�ͼ
            //����Ƕ��ͼƬ����߿�
            cb.SetLineWidth(1f);
            cb.Rectangle((Xleading + Xdelta), (842f - Yleading - Ydelta * 4f - 480f), (595f - (Xleading + Xdelta) * 2f), 280f);
            cb.Stroke();

            //Ƕ���ͼ
            int newW = (int)(595f - (Xleading + Xdelta * 1.5f) * 2f);
            int newH = (int)270f;
                        
            MemoryStream mem = new MemoryStream();
            data.Image.Save(mem, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] bytes = mem.ToArray();

            iTextSharp.text.Image img2 = iTextSharp.text.Image.GetInstance(bytes);
            img2.SetAbsolutePosition((Xleading + Xdelta*1.5f), (842f - Yleading - Ydelta * 4f - 475f));

            img2.ScaleAbsolute(newW, newH);

            cb.AddImage(img2);
            #endregion

        }

        private void DrawReportParams(Document doc,
                                  PdfContentByte cb,
                                  BaseFont bFont,
                                  float Xleading,
                                  float Yleading,
                                  float Xdelta,
                                  float Ydelta,
                                      bool flag)
        {
            string s;

            #region ���Ʊ����������
            //������ʣ�MHz
            //��ʼƵ�ʣ�MHz         ����Ƶ�ʣ�MHZ
            //XXX���ֵ��dBc        XXX��Сֵ��dBc
            //XXX�ο�ֵ��dBc
            //XXX���ۣ�PASS/FAIL

            cb.BeginText();

            s = "������ʣ�" + data.Tx_out.ToString("#.#") + " dBm";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta), (842f - Yleading - Ydelta * 5.5f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 70), (842f - Yleading - Ydelta * 5.5f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 145), (842f - Yleading - Ydelta * 5.5f - 480f - 2f));
            cb.Stroke();

            s = "��ʼƵ�ʣ�" + data.F_start.ToString("#.#") + " MHz";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta), (842f - Yleading - Ydelta * 7.0f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 70), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 155), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.Stroke();

            s = "��ֹƵ�ʣ�" + data.F_stop.ToString("#.#") + " MHz";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta + 200f), (842f - Yleading - Ydelta * 7f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 200f + 70), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 200f + 155), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.Stroke();

            s = "����������" + data.Points_Num.ToString();
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta + 200f * 2f), (842f - Yleading - Ydelta * 7f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 200f * 2f + 70), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 200f * 2f + 100), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.Stroke();
            if (data.Max_value == -200 || data.Max_value < float.MinValue || data.Max_value == float.MinValue)
            {
                s = "�������ֵ��######";
            }
            else
            {
                if (flag)
                    s = "�������ֵ��" + data.Max_value.ToString("#.#") + " dBc";
                else
                    s = "�������ֵ��" + data.Max_value.ToString("#.#") + " dBm";
            }
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta), (842f - Yleading - Ydelta * 8.5f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 85), (842f - Yleading - Ydelta * 8.5f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 170), (842f - Yleading - Ydelta * 8.5f - 480f - 2f));
            cb.Stroke();
            if (data.Min_value == -200 || data.Min_value > float.MaxValue || data.Min_value == float.MaxValue)
            {
                s = "������Сֵ��######";
            }
            else
            {
                if (flag)
                    s = "������Сֵ��" + data.Min_value.ToString("#.#") + " dBc";
                else
                    s = "������Сֵ��" + data.Min_value.ToString("#.#") + " dBm";
            }
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta + 200f), (842f - Yleading - Ydelta * 8.5f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 85 + 200f), (842f - Yleading - Ydelta * 8.5f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 170 + 200f), (842f - Yleading - Ydelta * 8.5f - 480f - 2f));
            cb.Stroke();

            if(flag)
                s = "�����ο�ֵ��" + data.Limit_value.ToString("#.#") + " dBc";
            else
                s = "�����ο�ֵ��" + data.Limit_value.ToString("#.#") + " dBm";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta), (842f - Yleading - Ydelta * 10f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 85), (842f - Yleading - Ydelta * 10f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 170), (842f - Yleading - Ydelta * 10f - 480f - 2f));
            cb.Stroke();


            s = "���Խ��ۣ�" + data.Passed;
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta + 200f), (842f - Yleading - Ydelta * 10f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 200f + 70), (842f - Yleading - Ydelta * 10f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 200f + 110), (842f - Yleading - Ydelta * 10f - 480f - 2f));
            cb.Stroke();


            s = "����ǩ�֣�";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta + 200f), (842f - Yleading - Ydelta * 11.5f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 200f + 70), (842f - Yleading - Ydelta * 11.5f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 200f + 170), (842f - Yleading - Ydelta * 11.5f - 480f - 2f));
            cb.Stroke();

            cb.EndText();
            #endregion

        }

        private void DrawReportFooter(PdfContentByte cb,
                               BaseFont bFont,
                               float Xleading,
                               float Yleading,
                               float Xdelta,
                               float Ydelta)
        {
            BaseColor col;
            string s;

            #region ���Ʊ���β��
            //���ƽ��·���ɫ��
            col = new BaseColor(0, 128, 128);
            cb.SetColorFill(col);
            cb.Rectangle((Xleading + Xdelta * 1.5f), (Yleading + Ydelta * 1.7f), (595f - (Xleading + Xdelta * 1.5f) * 2f), 20f);
            cb.Fill();
            cb.Stroke();

            //���ƽ��·���ɫ���ϵ�����
            cb.BeginText();
            col = new BaseColor(0, 0, 0);
            cb.SetColorFill(col);
            s = data.Footer;
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_CENTER, s, 595 / 2, (Yleading + Ydelta * 1.9f), 0);
            cb.EndText();

            //�����·���ֱ��
            cb.SetLineWidth(4f);
            cb.MoveTo(Xleading, (Yleading + Ydelta));
            cb.LineTo((595f - Xleading), (Yleading + Ydelta));
            cb.Stroke();

            //Ƕ�빫˾logo      
            System.Drawing.Image img = System.Drawing.Image.FromFile("logo.gif");
            MemoryStream mem = new MemoryStream();
            img.Save(mem, System.Drawing.Imaging.ImageFormat.Gif);
            byte[] bytes = mem.ToArray();

            iTextSharp.text.Image img2 = iTextSharp.text.Image.GetInstance(bytes);
            img2.SetAbsolutePosition(465, 10);
            img2.ScalePercent(10);
            cb.AddImage(img2);
            #endregion
        }
 
    }

    class PdfReport_Iso
    {
        private PdfReport_Data data;

        public void Do_Print(string fileName, PdfReport_Data data)
        {
            this.data = data;

            Document document = new Document(PageSize.A4, 0, 0, 0, 0);

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));

                writer.ViewerPreferences = (PdfWriter.CenterWindow | PdfWriter.FitWindow | PdfWriter.PageModeUseNone);

                document.Open();

                //ʹ����������
                BaseFont baseFont = BaseFont.CreateFont("C:\\WINDOWS\\FONTS\\SIMKAI.TTF",
                                                        BaseFont.IDENTITY_H,
                                                        BaseFont.NOT_EMBEDDED);

                PdfContentByte cb = writer.DirectContent;


                #region ����ǰ���ո������
                float Xleading = 27.5f;
                float Yleading = 27.5f;
                float Xdelta = 10f;
                float Ydelta = 20f;
                #endregion

                DrawReportHeader(cb, baseFont, Xleading, Yleading, Xdelta, Ydelta);

                DrawReportAbstract(cb, baseFont, Xleading, Yleading, Xdelta, Ydelta);

                DrawReportBody(cb, baseFont, Xleading, Yleading, Xdelta, Ydelta);

                DrawReportParams(document, cb, baseFont, Xleading, Yleading, Xdelta, Ydelta);

                DrawReportFooter(cb, baseFont, Xleading, Yleading, Xdelta, Ydelta);

                document.Close();

            }
            catch (Exception de)
            {
                Log2File(de.StackTrace);
            }
        }

        private void Log2File(string s)
        {
            StreamWriter sw = new StreamWriter("c:\\my_iso.log", true, Encoding.ASCII);

            sw.WriteLine(s);
            sw.Flush();
            sw.Close();
        }  

        private void DrawReportHeader(PdfContentByte cb,
                                      BaseFont bFont,
                                      float Xleading,
                                      float Yleading,
                                      float Xdelta,
                                      float Ydelta)
        {
            #region ���Ʊ���ͷ��
            //���Ʊ�ͷ��������ʱ��
            cb.BeginText();
            cb.SetTextMatrix(Xleading, (842f - Xleading));
            cb.SetFontAndSize(bFont, 15);
            cb.ShowText("Date Time: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            cb.EndText();

            //�����Ϸ���ֱ��
            cb.SetLineWidth(4f);
            cb.MoveTo(Xleading, (842 - Yleading - Ydelta));
            cb.LineTo((595f - Xleading), (842f - Yleading - Ydelta));
            cb.Stroke();

            //���ƽ��·�ϸֱ��
            cb.SetLineWidth(1f);
            cb.MoveTo((Xleading + Xdelta), (842f - Yleading - Ydelta * 2f));
            cb.LineTo((595f / 2f - 90f), (842f - Yleading - Ydelta * 2f));

            cb.MoveTo((595f / 2f + 90), (842f - Yleading - Ydelta * 2f));
            cb.LineTo((595f - Xleading - Xdelta), (842f - Yleading - Ydelta * 2f));
            cb.Stroke();

            //���ƽ��·�ϸֱ���ϵ�����
            cb.BeginText();
            string s = "ISOLATION TEST ABSTRACT";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_CENTER, s, 595 / 2, (842f - Yleading - Ydelta - 25f), 0);
            cb.EndText();
            #endregion
        }

        private void DrawReportAbstract(PdfContentByte cb,
                                        BaseFont bFont,
                                        float Xleading,
                                        float Yleading,
                                        float Xdelta,
                                        float Ydelta)
        {
            string s;
            BaseColor col;

            #region ���Ʊ���ժҪ����
            //���ƴ����
            cb.SetLineWidth(1f);
            cb.Rectangle((Xleading + Xdelta), (842f - Yleading - Ydelta * 3f - 180f), (595f - (Xleading + Xdelta) * 2f), 180f);
            cb.Stroke();

            //��������ڵ�С��
            col = new BaseColor(0, 128, 128);
            cb.SetColorFill(col);
            cb.Rectangle((Xleading + Xdelta * 1.5f), (842f - Yleading - Ydelta * 3f - 175f), (595f - (Xleading + Xdelta * 1.5f) * 2f), 170f);
            cb.Fill();
            cb.Stroke();

            //��С���ϻ���ժҪ���֣�����Description��Model NO.��Serial NO.��Operator
            col = new BaseColor(0, 0, 0);
            cb.SetColorFill(col);
            cb.SetFontAndSize(bFont, 15);

            cb.BeginText();
            s = "Description";
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 2f), (842f - Yleading - Ydelta * 3f - 25f), 0);

            s = "Model NO.";
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 2f), (842f - Yleading - Ydelta * 3f - 65f), 0);

            s = "Serial NO.";
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 2f), (842f - Yleading - Ydelta * 3f - 105f), 0);

            s = "Operator";
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 2f), (842f - Yleading - Ydelta * 3f - 145f), 0);


            s = data.Desc;
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 5.5f), (842f - Yleading - Ydelta * 3f - 40f), 0);

            s = data.Modno;
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 5.5f), (842f - Yleading - Ydelta * 3f - 80f), 0);

            s = data.Serno;
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 5.5f), (842f - Yleading - Ydelta * 3f - 120f), 0);

            s = data.Opeor;
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 5.5f), (842f - Yleading - Ydelta * 3f - 160f), 0);
            cb.EndText();


            //�����м�ϸֱ��
            cb.SetLineWidth(1f);
            cb.MoveTo((Xleading + Xdelta), (842f - Yleading - Ydelta * 4f - 180f));
            cb.LineTo((595f / 2f - 90f), (842f - Yleading - Ydelta * 4f - 180f));

            cb.MoveTo((595f / 2f + 90f), (842f - Yleading - Ydelta * 4f - 180f));
            cb.LineTo((595f - Xleading - Xdelta), (842f - Yleading - Ydelta * 4f - 180f));
            cb.Stroke();

            //�����м�ϸֱ���ϵ�����
            cb.BeginText();
            col = new BaseColor(0, 0, 0);
            cb.SetColorFill(col);
            s = "ISOLATION TEST DETAILS";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_CENTER, s, 595 / 2, (842f - Yleading - Ydelta * 4.3f - 180f), 0);
            cb.EndText();
            #endregion
        }

        private void DrawReportBody(PdfContentByte cb,
                                    BaseFont bFont,
                                     float Xleading,
                                     float Yleading,
                                     float Xdelta,
                                     float Ydelta)
        {

            #region ���Ʊ������岿�֣���Ƕ����ԵĽ�ͼ
            //����Ƕ��ͼƬ����߿�
            cb.SetLineWidth(1f);
            cb.Rectangle((Xleading + Xdelta), (842f - Yleading - Ydelta * 4f - 480f), (595f - (Xleading + Xdelta) * 2f), 280f);
            cb.Stroke();

            //Ƕ���ͼ
            int newW = (int)(595f - (Xleading + Xdelta * 1.5f) * 2f);
            int newH = (int)270f;
            
            MemoryStream mem = new MemoryStream();
            data.Image.Save(mem, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] bytes = mem.ToArray();

            iTextSharp.text.Image img2 = iTextSharp.text.Image.GetInstance(bytes);
            img2.SetAbsolutePosition((Xleading + Xdelta * 1.5f), (842f - Yleading - Ydelta * 4f - 475f));

            img2.ScaleAbsolute(newW, newH);

            cb.AddImage(img2);
            #endregion

        }

        private void DrawReportParams(Document doc,
                                  PdfContentByte cb,
                                  BaseFont bFont,
                                  float Xleading,
                                  float Yleading,
                                  float Xdelta,
                                  float Ydelta)
        {
            string s;
            
            #region ���Ʊ����������
            //������ʣ�MHz
            //��ʼƵ�ʣ�MHz         ����Ƶ�ʣ�MHZ
            //XXX���ֵ��dBc        XXX��Сֵ��dBc
            //XXX�ο�ֵ��dBc
            //XXX���ۣ�PASS/FAIL

            cb.BeginText();

            s = "������ʣ�" + data.Tx_out.ToString("#.#") + " dBm";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta), (842f - Yleading - Ydelta * 5.5f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 70), (842f - Yleading - Ydelta * 5.5f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 145), (842f - Yleading - Ydelta * 5.5f - 480f - 2f));
            cb.Stroke();

            s = "��ʼƵ�ʣ�" + data.F_start.ToString("#.#") + " MHz";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta), (842f - Yleading - Ydelta * 7.0f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 70), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 155), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.Stroke();

            s = "��ֹƵ�ʣ�" + data.F_stop.ToString("#.#") + " MHz";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta + 200f), (842f - Yleading - Ydelta * 7f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 200f + 70), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 200f + 155), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.Stroke();

            s = "����������" + data.Points_Num.ToString();
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta + 200f * 2f), (842f - Yleading - Ydelta * 7f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 200f * 2f + 70), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 200f * 2f + 100), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.Stroke();


            s = "��������ֵ��" + data.Max_value.ToString("#.#") + " dBc";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta), (842f - Yleading - Ydelta * 8.5f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 100), (842f - Yleading - Ydelta * 8.5f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 185), (842f - Yleading - Ydelta * 8.5f - 480f - 2f));
            cb.Stroke();

            s = "�������Сֵ��" + data.Min_value.ToString("#.#") + " dBc";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta + 200f), (842f - Yleading - Ydelta * 8.5f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 100 + 200f), (842f - Yleading - Ydelta * 8.5f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 185 + 200f), (842f - Yleading - Ydelta * 8.5f - 480f - 2f));
            cb.Stroke();


            s = "����Ȳο�ֵ��" + data.Limit_value.ToString("#.#") + " dBc";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta), (842f - Yleading - Ydelta * 10f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 100), (842f - Yleading - Ydelta * 10f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 185), (842f - Yleading - Ydelta * 10f - 480f - 2f));
            cb.Stroke();


            s = "���Խ��ۣ�" + data.Passed;
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta + 200f), (842f - Yleading - Ydelta * 10f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 200f + 70), (842f - Yleading - Ydelta * 10f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 200f + 110), (842f - Yleading - Ydelta * 10f - 480f - 2f));
            cb.Stroke();


            s = "����ǩ�֣�";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta + 200f), (842f - Yleading - Ydelta * 11.5f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 200f + 70), (842f - Yleading - Ydelta * 11.5f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 200f + 170), (842f - Yleading - Ydelta * 11.5f - 480f - 2f));
            cb.Stroke();

            cb.EndText();
            #endregion

        }

        private void DrawReportFooter(PdfContentByte cb,
                               BaseFont bFont,
                               float Xleading,
                               float Yleading,
                               float Xdelta,
                               float Ydelta)
        {
            BaseColor col;
            string s;

            #region ���Ʊ���β��
            //���ƽ��·���ɫ��
            col = new BaseColor(0, 128, 128);
            cb.SetColorFill(col);
            cb.Rectangle((Xleading + Xdelta * 1.5f), (Yleading + Ydelta * 1.7f), (595f - (Xleading + Xdelta * 1.5f) * 2f), 20f);
            cb.Fill();
            cb.Stroke();

            //���ƽ��·���ɫ���ϵ�����
            cb.BeginText();
            col = new BaseColor(0, 0, 0);
            cb.SetColorFill(col);
            s = data.Footer;
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_CENTER, s, 595 / 2, (Yleading + Ydelta * 1.9f), 0);
            cb.EndText();

            //�����·���ֱ��
            cb.SetLineWidth(4f);
            cb.MoveTo(Xleading, (Yleading + Ydelta));
            cb.LineTo((595f - Xleading), (Yleading + Ydelta));
            cb.Stroke();

            //Ƕ�빫˾logo      
            System.Drawing.Image img = System.Drawing.Image.FromFile("logo.gif");
            MemoryStream mem = new MemoryStream();
            img.Save(mem, System.Drawing.Imaging.ImageFormat.Gif);
            byte[] bytes = mem.ToArray();

            iTextSharp.text.Image img2 = iTextSharp.text.Image.GetInstance(bytes);
            img2.SetAbsolutePosition(465, 10);
            img2.ScalePercent(10);
            cb.AddImage(img2);
            #endregion
        }

    }

    class PdfReport_Vsw
    {
        private PdfReport_Data data;

        public void Do_Print(string fileName, PdfReport_Data data)
        {
            this.data = data;

            Document document = new Document(PageSize.A4, 0, 0, 0, 0);

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));

                writer.ViewerPreferences = (PdfWriter.CenterWindow | PdfWriter.FitWindow | PdfWriter.PageModeUseNone);

                document.Open();

                //ʹ����������
                BaseFont baseFont = BaseFont.CreateFont("C:\\WINDOWS\\FONTS\\SIMKAI.TTF",
                                                        BaseFont.IDENTITY_H,
                                                        BaseFont.NOT_EMBEDDED);

                PdfContentByte cb = writer.DirectContent;


                #region ����ǰ���ո������
                float Xleading = 27.5f;
                float Yleading = 27.5f;
                float Xdelta = 10f;
                float Ydelta = 20f;
                #endregion

                DrawReportHeader(cb, baseFont, Xleading, Yleading, Xdelta, Ydelta);

                DrawReportAbstract(cb, baseFont, Xleading, Yleading, Xdelta, Ydelta);

                DrawReportBody(cb, baseFont, Xleading, Yleading, Xdelta, Ydelta);

                DrawReportParams(document, cb, baseFont, Xleading, Yleading, Xdelta, Ydelta);

                DrawReportFooter(cb, baseFont, Xleading, Yleading, Xdelta, Ydelta);

                document.Close();

            }
            catch (Exception de)
            {
                Console.WriteLine(de.StackTrace);
            }
        }


        private void DrawReportHeader(PdfContentByte cb,
                                      BaseFont bFont,
                                      float Xleading,
                                      float Yleading,
                                      float Xdelta,
                                      float Ydelta)
        {
            #region ���Ʊ���ͷ��
            //���Ʊ�ͷ��������ʱ��
            cb.BeginText();
            cb.SetTextMatrix(Xleading, (842f - Xleading));
            cb.SetFontAndSize(bFont, 15);
            cb.ShowText("Date Time: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            cb.EndText();

            //�����Ϸ���ֱ��
            cb.SetLineWidth(4f);
            cb.MoveTo(Xleading, (842 - Yleading - Ydelta));
            cb.LineTo((595f - Xleading), (842f - Yleading - Ydelta));
            cb.Stroke();

            //���ƽ��·�ϸֱ��
            cb.SetLineWidth(1f);
            cb.MoveTo((Xleading + Xdelta), (842f - Yleading - Ydelta * 2f));
            cb.LineTo((595f / 2f - 70f), (842f - Yleading - Ydelta * 2f));

            cb.MoveTo((595f / 2f + 70), (842f - Yleading - Ydelta * 2f));
            cb.LineTo((595f - Xleading - Xdelta), (842f - Yleading - Ydelta * 2f));
            cb.Stroke();

            //���ƽ��·�ϸֱ���ϵ�����
            cb.BeginText();
            string s = "VSWR TEST ABSTRACT";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_CENTER, s, 595 / 2, (842f - Yleading - Ydelta - 25f), 0);
            cb.EndText();
            #endregion
        }

        private void DrawReportAbstract(PdfContentByte cb,
                                        BaseFont bFont,
                                        float Xleading,
                                        float Yleading,
                                        float Xdelta,
                                        float Ydelta)
        {
            string s;
            BaseColor col;

            #region ���Ʊ���ժҪ����
            //���ƴ����
            cb.SetLineWidth(1f);
            cb.Rectangle((Xleading + Xdelta), (842f - Yleading - Ydelta * 3f - 180f), (595f - (Xleading + Xdelta) * 2f), 180f);
            cb.Stroke();

            //��������ڵ�С��
            col = new BaseColor(0, 128, 128);
            cb.SetColorFill(col);
            cb.Rectangle((Xleading + Xdelta * 1.5f), (842f - Yleading - Ydelta * 3f - 175f), (595f - (Xleading + Xdelta * 1.5f) * 2f), 170f);
            cb.Fill();
            cb.Stroke();

            //��С���ϻ���ժҪ���֣�����Description��Model NO.��Serial NO.��Operator
            col = new BaseColor(0, 0, 0);
            cb.SetColorFill(col);
            cb.SetFontAndSize(bFont, 15);

            cb.BeginText();
            s = "Description";
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 2f), (842f - Yleading - Ydelta * 3f - 25f), 0);

            s = "Model NO.";
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 2f), (842f - Yleading - Ydelta * 3f - 65f), 0);

            s = "Serial NO.";
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 2f), (842f - Yleading - Ydelta * 3f - 105f), 0);

            s = "Operator";
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 2f), (842f - Yleading - Ydelta * 3f - 145f), 0);


            s = data.Desc;
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 5.5f), (842f - Yleading - Ydelta * 3f - 40f), 0);

            s = data.Modno;
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 5.5f), (842f - Yleading - Ydelta * 3f - 80f), 0);

            s = data.Serno;
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 5.5f), (842f - Yleading - Ydelta * 3f - 120f), 0);

            s = data.Opeor;
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 5.5f), (842f - Yleading - Ydelta * 3f - 160f), 0);
            cb.EndText();


            //�����м�ϸֱ��
            cb.SetLineWidth(1f);
            cb.MoveTo((Xleading + Xdelta), (842f - Yleading - Ydelta * 4f - 180f));
            cb.LineTo((595f / 2f - 70f), (842f - Yleading - Ydelta * 4f - 180f));

            cb.MoveTo((595f / 2f + 70f), (842f - Yleading - Ydelta * 4f - 180f));
            cb.LineTo((595f - Xleading - Xdelta), (842f - Yleading - Ydelta * 4f - 180f));
            cb.Stroke();

            //�����м�ϸֱ���ϵ�����
            cb.BeginText();
            col = new BaseColor(0, 0, 0);
            cb.SetColorFill(col);
            s = "VSWR TEST DETAILS";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_CENTER, s, 595 / 2, (842f - Yleading - Ydelta * 4.3f - 180f), 0);
            cb.EndText();
            #endregion
        }

        private void DrawReportBody(PdfContentByte cb,
                                    BaseFont bFont,
                                     float Xleading,
                                     float Yleading,
                                     float Xdelta,
                                     float Ydelta)
        {

            #region ���Ʊ������岿�֣���Ƕ����ԵĽ�ͼ
            //����Ƕ��ͼƬ����߿�
            cb.SetLineWidth(1f);
            cb.Rectangle((Xleading + Xdelta), (842f - Yleading - Ydelta * 4f - 480f), (595f - (Xleading + Xdelta) * 2f), 280f);
            cb.Stroke();

            //Ƕ���ͼ
            int newW = (int)(595f - (Xleading + Xdelta * 1.5f) * 2f);
            int newH = (int)270f;

            MemoryStream mem = new MemoryStream();
            data.Image.Save(mem, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] bytes = mem.ToArray();

            iTextSharp.text.Image img2 = iTextSharp.text.Image.GetInstance(bytes);
            img2.SetAbsolutePosition((Xleading + Xdelta * 1.5f), (842f - Yleading - Ydelta * 4f - 475f));

            img2.ScaleAbsolute(newW, newH);

            cb.AddImage(img2);
            #endregion

        }

        private void DrawReportParams(Document doc,
                                  PdfContentByte cb,
                                  BaseFont bFont,
                                  float Xleading,
                                  float Yleading,
                                  float Xdelta,
                                  float Ydelta)
        {
            string s;

            #region ���Ʊ����������
            //������ʣ�MHz
            //��ʼƵ�ʣ�MHz         ����Ƶ�ʣ�MHZ
            //XXX���ֵ��dBc        XXX��Сֵ��dBc
            //XXX�ο�ֵ��dBc
            //XXX���ۣ�PASS/FAIL

            cb.BeginText();

            s = "������ʣ�" + data.Tx_out.ToString("#.#") + " dBm";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta), (842f - Yleading - Ydelta * 5.5f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 70), (842f - Yleading - Ydelta * 5.5f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 145), (842f - Yleading - Ydelta * 5.5f - 480f - 2f));
            cb.Stroke();

            s = "��ʼƵ�ʣ�" + data.F_start.ToString("#.#") + " MHz";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta), (842f - Yleading - Ydelta * 7.0f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 70), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 155), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.Stroke();

            s = "��ֹƵ�ʣ�" + data.F_stop.ToString("#.#") + " MHz";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta + 200f), (842f - Yleading - Ydelta * 7f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 200f + 70), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 200f + 155), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.Stroke();

            s = "����������" + data.Points_Num.ToString();
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta + 200f * 2f), (842f - Yleading - Ydelta * 7f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 200f * 2f + 70), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 200f * 2f + 100), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.Stroke();


            s = "פ�������ֵ��" + data.Max_value.ToString("0.000") + "";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta), (842f - Yleading - Ydelta * 8.5f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 100), (842f - Yleading - Ydelta * 8.5f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 185), (842f - Yleading - Ydelta * 8.5f - 480f - 2f));
            cb.Stroke();

            s = "פ������Сֵ��" + data.Min_value.ToString("0.000");
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta + 200f), (842f - Yleading - Ydelta * 8.5f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 100 + 200f), (842f - Yleading - Ydelta * 8.5f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 185 + 200f), (842f - Yleading - Ydelta * 8.5f - 480f - 2f));
            cb.Stroke();


            s = "פ���Ȳο�ֵ��" + data.Limit_value.ToString("#.#");
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta), (842f - Yleading - Ydelta * 10f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 100), (842f - Yleading - Ydelta * 10f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 185), (842f - Yleading - Ydelta * 10f - 480f - 2f));
            cb.Stroke();


            s = "���Խ��ۣ�" + data.Passed;
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta + 200f), (842f - Yleading - Ydelta * 10f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 200f + 70), (842f - Yleading - Ydelta * 10f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 200f + 110), (842f - Yleading - Ydelta * 10f - 480f - 2f));
            cb.Stroke();


            s = "����ǩ�֣�";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta + 200f), (842f - Yleading - Ydelta * 11.5f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 200f + 70), (842f - Yleading - Ydelta * 11.5f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 200f + 170), (842f - Yleading - Ydelta * 11.5f - 480f - 2f));
            cb.Stroke();

            cb.EndText();
            #endregion

        }

        private void DrawReportFooter(PdfContentByte cb,
                               BaseFont bFont,
                               float Xleading,
                               float Yleading,
                               float Xdelta,
                               float Ydelta)
        {
            BaseColor col;
            string s;

            #region ���Ʊ���β��
            //���ƽ��·���ɫ��
            col = new BaseColor(0, 128, 128);
            cb.SetColorFill(col);
            cb.Rectangle((Xleading + Xdelta * 1.5f), (Yleading + Ydelta * 1.7f), (595f - (Xleading + Xdelta * 1.5f) * 2f), 20f);
            cb.Fill();
            cb.Stroke();

            //���ƽ��·���ɫ���ϵ�����
            cb.BeginText();
            col = new BaseColor(0, 0, 0);
            cb.SetColorFill(col);
            s = data.Footer;
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_CENTER, s, 595 / 2, (Yleading + Ydelta * 1.9f), 0);
            cb.EndText();

            //�����·���ֱ��
            cb.SetLineWidth(4f);
            cb.MoveTo(Xleading, (Yleading + Ydelta));
            cb.LineTo((595f - Xleading), (Yleading + Ydelta));
            cb.Stroke();

            //Ƕ�빫˾logo      
            System.Drawing.Image img = System.Drawing.Image.FromFile("logo.gif");
            MemoryStream mem = new MemoryStream();
            img.Save(mem, System.Drawing.Imaging.ImageFormat.Gif);
            byte[] bytes = mem.ToArray();

            iTextSharp.text.Image img2 = iTextSharp.text.Image.GetInstance(bytes);
            img2.SetAbsolutePosition(465, 10);
            img2.ScalePercent(10);
            cb.AddImage(img2);
            #endregion
        }

    }

    class PdfReport_Har
    {
        private PdfReport_Data data;

        public void Do_Print(string fileName, PdfReport_Data data)
        {
            this.data = data;

            Document document = new Document(PageSize.A4, 0, 0, 0, 0);

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));

                writer.ViewerPreferences = (PdfWriter.CenterWindow | PdfWriter.FitWindow | PdfWriter.PageModeUseNone);

                document.Open();

                //ʹ����������
                BaseFont baseFont = BaseFont.CreateFont("C:\\WINDOWS\\FONTS\\SIMKAI.TTF",
                                                        BaseFont.IDENTITY_H,
                                                        BaseFont.NOT_EMBEDDED);

                PdfContentByte cb = writer.DirectContent;


                #region ����ǰ���ո������
                float Xleading = 27.5f;
                float Yleading = 27.5f;
                float Xdelta = 10f;
                float Ydelta = 20f;
                #endregion

                DrawReportHeader(cb, baseFont, Xleading, Yleading, Xdelta, Ydelta);

                DrawReportAbstract(cb, baseFont, Xleading, Yleading, Xdelta, Ydelta);

                DrawReportBody(cb, baseFont, Xleading, Yleading, Xdelta, Ydelta);

                DrawReportParams(document, cb, baseFont, Xleading, Yleading, Xdelta, Ydelta);

                DrawReportFooter(cb, baseFont, Xleading, Yleading, Xdelta, Ydelta);

                document.Close();

            }
            catch (Exception de)
            {
                Console.WriteLine(de.StackTrace);
            }
        }


        private void DrawReportHeader(PdfContentByte cb,
                                      BaseFont bFont,
                                      float Xleading,
                                      float Yleading,
                                      float Xdelta,
                                      float Ydelta)
        {
            #region ���Ʊ���ͷ��
            //���Ʊ�ͷ��������ʱ��
            cb.BeginText();
            cb.SetTextMatrix(Xleading, (842f - Xleading));
            cb.SetFontAndSize(bFont, 15);
            cb.ShowText("Date Time: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            cb.EndText();

            //�����Ϸ���ֱ��
            cb.SetLineWidth(4f);
            cb.MoveTo(Xleading, (842 - Yleading - Ydelta));
            cb.LineTo((595f - Xleading), (842f - Yleading - Ydelta));
            cb.Stroke();

            //���ƽ��·�ϸֱ��
            cb.SetLineWidth(1f);
            cb.MoveTo((Xleading + Xdelta), (842f - Yleading - Ydelta * 2f));
            cb.LineTo((595f / 2f - 90f), (842f - Yleading - Ydelta * 2f));

            cb.MoveTo((595f / 2f + 90), (842f - Yleading - Ydelta * 2f));
            cb.LineTo((595f - Xleading - Xdelta), (842f - Yleading - Ydelta * 2f));
            cb.Stroke();

            //���ƽ��·�ϸֱ���ϵ�����
            cb.BeginText();
            string s = "HARMONIC TEST ABSTRACT";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_CENTER, s, 595 / 2, (842f - Yleading - Ydelta - 25f), 0);
            cb.EndText();
            #endregion
        }

        private void DrawReportAbstract(PdfContentByte cb,
                                        BaseFont bFont,
                                        float Xleading,
                                        float Yleading,
                                        float Xdelta,
                                        float Ydelta)
        {
            string s;
            BaseColor col;

            #region ���Ʊ���ժҪ����
            //���ƴ����
            cb.SetLineWidth(1f);
            cb.Rectangle((Xleading + Xdelta), (842f - Yleading - Ydelta * 3f - 180f), (595f - (Xleading + Xdelta) * 2f), 180f);
            cb.Stroke();

            //��������ڵ�С��
            col = new BaseColor(0, 128, 128);
            cb.SetColorFill(col);
            cb.Rectangle((Xleading + Xdelta * 1.5f), (842f - Yleading - Ydelta * 3f - 175f), (595f - (Xleading + Xdelta * 1.5f) * 2f), 170f);
            cb.Fill();
            cb.Stroke();

            //��С���ϻ���ժҪ���֣�����Description��Model NO.��Serial NO.��Operator
            col = new BaseColor(0, 0, 0);
            cb.SetColorFill(col);
            cb.SetFontAndSize(bFont, 15);

            cb.BeginText();
            s = "Description";
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 2f), (842f - Yleading - Ydelta * 3f - 25f), 0);

            s = "Model NO.";
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 2f), (842f - Yleading - Ydelta * 3f - 65f), 0);

            s = "Serial NO.";
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 2f), (842f - Yleading - Ydelta * 3f - 105f), 0);

            s = "Operator";
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 2f), (842f - Yleading - Ydelta * 3f - 145f), 0);


            s = data.Desc;
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 5.5f), (842f - Yleading - Ydelta * 3f - 40f), 0);

            s = data.Modno;
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 5.5f), (842f - Yleading - Ydelta * 3f - 80f), 0);

            s = data.Serno;
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 5.5f), (842f - Yleading - Ydelta * 3f - 120f), 0);

            s = data.Opeor;
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta * 5.5f), (842f - Yleading - Ydelta * 3f - 160f), 0);
            cb.EndText();


            //�����м�ϸֱ��
            cb.SetLineWidth(1f);
            cb.MoveTo((Xleading + Xdelta), (842f - Yleading - Ydelta * 4f - 180f));
            cb.LineTo((595f / 2f - 90f), (842f - Yleading - Ydelta * 4f - 180f));

            cb.MoveTo((595f / 2f + 90f), (842f - Yleading - Ydelta * 4f - 180f));
            cb.LineTo((595f - Xleading - Xdelta), (842f - Yleading - Ydelta * 4f - 180f));
            cb.Stroke();

            //�����м�ϸֱ���ϵ�����
            cb.BeginText();
            col = new BaseColor(0, 0, 0);
            cb.SetColorFill(col);
            s = "HARMONIC TEST DETAILS";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_CENTER, s, 595 / 2, (842f - Yleading - Ydelta * 4.3f - 180f), 0);
            cb.EndText();
            #endregion
        }

        private void DrawReportBody(PdfContentByte cb,
                                    BaseFont bFont,
                                     float Xleading,
                                     float Yleading,
                                     float Xdelta,
                                     float Ydelta)
        {

            #region ���Ʊ������岿�֣���Ƕ����ԵĽ�ͼ
            //����Ƕ��ͼƬ����߿�
            cb.SetLineWidth(1f);
            cb.Rectangle((Xleading + Xdelta), (842f - Yleading - Ydelta * 4f - 480f), (595f - (Xleading + Xdelta) * 2f), 280f);
            cb.Stroke();

            //Ƕ���ͼ
            int newW = (int)(595f - (Xleading + Xdelta * 1.5f) * 2f);
            int newH = (int)270f;

            MemoryStream mem = new MemoryStream();
            data.Image.Save(mem, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] bytes = mem.ToArray();

            iTextSharp.text.Image img2 = iTextSharp.text.Image.GetInstance(bytes);
            img2.SetAbsolutePosition((Xleading + Xdelta * 1.5f), (842f - Yleading - Ydelta * 4f - 475f));

            img2.ScaleAbsolute(newW, newH);

            cb.AddImage(img2);
            #endregion
        }

        private void DrawReportParams(Document doc,
                                  PdfContentByte cb,
                                  BaseFont bFont,
                                  float Xleading,
                                  float Yleading,
                                  float Xdelta,
                                  float Ydelta)
        {
            string s;

            bFont.FontType = 12;

            #region ���Ʊ����������
            //������ʣ�MHz
            //��ʼƵ�ʣ�MHz         ����Ƶ�ʣ�MHZ
            //XXX���ֵ��dBc        XXX��Сֵ��dBc
            //XXX�ο�ֵ��dBc
            //XXX���ۣ�PASS/FAIL

            cb.BeginText();

            s = "������ʣ�" + data.Tx_out.ToString("#.#") + " dBm";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta), (842f - Yleading - Ydelta * 5.5f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 70), (842f - Yleading - Ydelta * 5.5f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 145), (842f - Yleading - Ydelta * 5.5f - 480f - 2f));
            cb.Stroke();

            s = "��ʼƵ�ʣ�" + data.F_start.ToString("#.#") + " MHz";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta), (842f - Yleading - Ydelta * 7.0f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 70), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 155), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.Stroke();

            s = "��ֹƵ�ʣ�" + data.F_stop.ToString("#.#") + " MHz";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta + 200f), (842f - Yleading - Ydelta * 7f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 200f + 70), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 200f + 155), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.Stroke();

            s = "����������" + data.Points_Num.ToString();
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta + 200f * 2f), (842f - Yleading - Ydelta * 7f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 200f * 2f + 70), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 200f * 2f + 100), (842f - Yleading - Ydelta * 7f - 480f - 2f));
            cb.Stroke();


            s = "г�����ֵ��" + data.Max_value.ToString("#.#") + " dBm";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta), (842f - Yleading - Ydelta * 8.5f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 85), (842f - Yleading - Ydelta * 8.5f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 170), (842f - Yleading - Ydelta * 8.5f - 480f - 2f));
            cb.Stroke();

            s = "г����Сֵ��" + data.Min_value.ToString("#.#") + " dBm";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta + 200f), (842f - Yleading - Ydelta * 8.5f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 85 + 200f), (842f - Yleading - Ydelta * 8.5f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 170 + 200f), (842f - Yleading - Ydelta * 8.5f - 480f - 2f));
            cb.Stroke();


            s = "г���ο�ֵ��" + data.Limit_value.ToString("#.#") + " dBm";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta), (842f - Yleading - Ydelta * 10f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 85), (842f - Yleading - Ydelta * 10f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 170), (842f - Yleading - Ydelta * 10f - 480f - 2f));
            cb.Stroke();


            s = "���Խ��ۣ�" + data.Passed;
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta + 200f), (842f - Yleading - Ydelta * 10f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 200f + 70), (842f - Yleading - Ydelta * 10f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 200f + 110), (842f - Yleading - Ydelta * 10f - 480f - 2f));
            cb.Stroke();


            s = "����ǩ�֣�";
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_LEFT, s, (Xleading + Xdelta + 200f), (842f - Yleading - Ydelta * 11.5f - 480f), 0);
            cb.MoveTo((Xleading + Xdelta + 200f + 70), (842f - Yleading - Ydelta * 11.5f - 480f - 2f));
            cb.LineTo((Xleading + Xdelta + 200f + 170), (842f - Yleading - Ydelta * 11.5f - 480f - 2f));
            cb.Stroke();

            cb.EndText();
            #endregion

        }

        private void DrawReportFooter(PdfContentByte cb,
                               BaseFont bFont,
                               float Xleading,
                               float Yleading,
                               float Xdelta,
                               float Ydelta)
        {
            BaseColor col;
            string s;

            #region ���Ʊ���β��
            //���ƽ��·���ɫ��
            col = new BaseColor(0, 128, 128);
            cb.SetColorFill(col);
            cb.Rectangle((Xleading + Xdelta * 1.5f), (Yleading + Ydelta * 1.7f), (595f - (Xleading + Xdelta * 1.5f) * 2f), 20f);
            cb.Fill();
            cb.Stroke();

            //���ƽ��·���ɫ���ϵ�����
            cb.BeginText();
            col = new BaseColor(0, 0, 0);
            cb.SetColorFill(col);
            s = data.Footer;
            cb.SetFontAndSize(bFont, 15);
            cb.ShowTextAligned(Element.ALIGN_CENTER, s, 595 / 2, (Yleading + Ydelta * 1.9f), 0);
            cb.EndText();

            //�����·���ֱ��
            cb.SetLineWidth(4f);
            cb.MoveTo(Xleading, (Yleading + Ydelta));
            cb.LineTo((595f - Xleading), (Yleading + Ydelta));
            cb.Stroke();

            //Ƕ�빫˾logo      
            System.Drawing.Image img = System.Drawing.Image.FromFile("logo.gif");
            MemoryStream mem = new MemoryStream();
            img.Save(mem, System.Drawing.Imaging.ImageFormat.Gif);
            byte[] bytes = mem.ToArray();

            iTextSharp.text.Image img2 = iTextSharp.text.Image.GetInstance(bytes);
            img2.SetAbsolutePosition(465, 10);
            img2.ScalePercent(10);
            cb.AddImage(img2);
            #endregion
        }

    }
}