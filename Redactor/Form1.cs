using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Redactor
{
    public partial class Form1 : Form
    {
        //����������
        Image image;
        //������� �� ����������
        bool isOpened = false;
        //�������� �����������.
        Image recoverImage;
        //��������� ��������� ������ � �����������.
        bool isScrolled;
        Image scrolledImage;
        //�������������� ����� �������.
        Image recoverAfterCut;
        //�������� �������.
        bool isCuted;
        //�������������� �� ��������� ������.
        Image recoverAfterColorChange;
        //���������� ������ ������� �����������.
        string imagePath = string.Empty;
        //����������� ����� ������ ������.
        Image firstColorReturn;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//�������
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files | *.jpg; *.jpeg; *.png; *.bmp";
            openFileDialog.InitialDirectory = @"C:\";

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFileDialog.FileName);
                imagePath = openFileDialog.FileName;
                recoverImage = image;
                pictureBox1.Image = image;
                isOpened = true;

                if(image.Width > pictureBox1.Width || image.Height > pictureBox1.Height)//����� �����������, ����� ����������� � picturebox
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                recoverAfterColorChange = image;
                firstColorReturn = image;
            }
        }

        private void button2_Click(object sender, EventArgs e)//���������
        {
            if(isOpened)
            {
                try
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Image Files | *.jpg; *.jpeg; *.png; *.bmp";
                    saveFileDialog.InitialDirectory = @"C:\";

                    ImageFormat format = ImageFormat.Png;//����������� ������

                    if(saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string extension = Path.GetExtension(saveFileDialog.FileName);
                        switch (extension)
                        {
                            case ".jpg":
                                format = ImageFormat.Jpeg;
                                break;
                            case ".bmp":
                                format = ImageFormat.Bmp;
                                break;
                        }
                        pictureBox1.Image.Save(saveFileDialog.FileName, format);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("����� ��������� �����������, ������� �������� ���");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    Transparency();
                    break;
                case 1:
                    Grayscale();
                    break;
                case 2:
                    SepiaTone();
                    break;
                case 3:
                    Negative();
                    break;
            }
        }

        #region �������.

        public void Transparency()
        {
            if(isOpened)
            {
                Image curImage = pictureBox1.Image;
                Bitmap bitmap = new Bitmap(curImage.Width, curImage.Height);
                ImageAttributes attributes = new ImageAttributes();

                ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                {
                    new float[]{1, 0, 0, 0, 0},
                    new float[]{0, 1, 0, 0, 0},
                    new float[]{0, 0, 1, 0, 0},
                    new float[]{0, 0, 0, 0.3f, 0},
                    new float[]{0, 0, 0, 0, 1}
                });

                attributes.SetColorMatrix(colorMatrix);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.DrawImage(curImage, new Rectangle(0, 0, curImage.Width, curImage.Height), 0, 0, curImage.Width, curImage.Height, GraphicsUnit.Pixel, attributes);

                graphics.Dispose();
                pictureBox1.Image = bitmap;
            }
            else
            {
                MessageBox.Show("������ ��� ��������� ������ �������� �����������");
            }
        }

        public void Grayscale()
        {
            if (isOpened)
            {
                Image curImage = pictureBox1.Image;
                Bitmap bitmap = new Bitmap(curImage.Width, curImage.Height);
                ImageAttributes attributes = new ImageAttributes();

                ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                {
                    new float[]{.3f, .3f, .3f, 0, 0},
                    new float[]{.59f, .59f, .59f, 0, 0},
                    new float[]{.11f, .11f, .11f, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}
                });

                attributes.SetColorMatrix(colorMatrix);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.DrawImage(curImage, new Rectangle(0, 0, curImage.Width, curImage.Height), 0, 0, curImage.Width, curImage.Height, GraphicsUnit.Pixel, attributes);

                graphics.Dispose();
                pictureBox1.Image = bitmap;
            }
            else
            {
                MessageBox.Show("������ ��� ��������� ������ �������� �����������");
            }
        }

        public void SepiaTone()
        {
            if (isOpened)
            {
                Image curImage = pictureBox1.Image;
                Bitmap bitmap = new Bitmap(curImage.Width, curImage.Height);
                ImageAttributes attributes = new ImageAttributes();

                ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                {
                    new float[]{.393f, .349f, .272f, 0, 0},
                    new float[]{.769f, .686f, .534f, 0, 0},
                    new float[]{.189f, .168f, .131f, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}
                });

                attributes.SetColorMatrix(colorMatrix);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.DrawImage(curImage, new Rectangle(0, 0, curImage.Width, curImage.Height), 0, 0, curImage.Width, curImage.Height, GraphicsUnit.Pixel, attributes);

                graphics.Dispose();
                pictureBox1.Image = bitmap;
            }
            else
            {
                MessageBox.Show("������ ��� ��������� ������ �������� �����������");
            }
        }

        public void Negative()
        {
            if (isOpened)
            {
                Image curImage = pictureBox1.Image;
                Bitmap bitmap = new Bitmap(curImage.Width, curImage.Height);
                ImageAttributes attributes = new ImageAttributes();

                ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                {
                    new float[]{-1, 0, 0, 0, 0},
                    new float[]{0, -1, 0, 0, 0},
                    new float[]{0, 0, -1, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{1, 1, 1, 1, 1}
                });

                attributes.SetColorMatrix(colorMatrix);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.DrawImage(curImage, new Rectangle(0, 0, curImage.Width, curImage.Height), 0, 0, curImage.Width, curImage.Height, GraphicsUnit.Pixel, attributes);

                graphics.Dispose();
                pictureBox1.Image = bitmap;
            }
            else
            {
                MessageBox.Show("������ ��� ��������� ������ �������� �����������");
            }
        }

        #endregion

        private void button3_Click(object sender, EventArgs e)//�������� ������
        {
            if(isScrolled)
            {
                pictureBox1.Image = scrolledImage;
            }
            else
            {
                image = recoverImage;
                pictureBox1.Image = image;
            }
        }

        private void RelodeImage()
        {
            image = recoverImage;
            pictureBox1.Image = image;
        }

        private void BarsChange()//��������� ��������� ������ �� ���������.
        {
            //��������� �������� � ���������.
            float changered = trackBar1.Value * 0.1f;
            float changegreen = trackBar2.Value * 0.1f;
            float changeblue = trackBar3.Value * 0.1f;

            RelodeImage();

            if(isOpened)
            {
                Image image = pictureBox1.Image;
                Bitmap bitmap = new Bitmap(image.Width, image.Height);

                ImageAttributes imageAttributes = new ImageAttributes();
                ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                {
                    new float[]{1 + changered, 0, 0, 0, 0},
                    new float[]{0, 1 + changegreen, 0, 0, 0},
                    new float[]{0, 0, 1 + changeblue, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}
                });

                imageAttributes.SetColorMatrix(colorMatrix);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);

                graphics.Dispose();
                pictureBox1.Image = bitmap;
                scrolledImage = pictureBox1.Image;
            }
            else
            {
                MessageBox.Show("�������� ����������� ������ ��� ��� �������������");
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)//�������
        {
            BarsChange();
            isScrolled = true;
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)//�������
        {
            BarsChange();
            isScrolled = true;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)//�����
        {
            BarsChange();
            isScrolled = true;
        }

        private void button4_Click(object sender, EventArgs e)//�������
        {
            if(isOpened)
            {
                try
                {
                    Image curImage = pictureBox1.Image;
                    recoverAfterCut = curImage;

                    int newHeight = Convert.ToInt32(textBox1.Text);
                    int newWidth = Convert.ToInt32(textBox2.Text);

                    //���������� ������ ��� ������ �����������
                    var destRect = new Rectangle(0, 0, newWidth, newHeight);
                    var destImage = new Bitmap(newWidth, newHeight);

                    destImage.SetResolution(curImage.HorizontalResolution, curImage.VerticalResolution);

                    using(var graphics = Graphics.FromImage(destImage))
                    {
                        //��������� �������� ��������� �������.
                        graphics.CompositingMode = CompositingMode.SourceCopy;//������ ������ ��������� ��������� �����������.
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        using (var wrapMode = new ImageAttributes())
                        {
                            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                            graphics.DrawImage(curImage, destRect, 0, 0, curImage.Width, curImage.Height, GraphicsUnit.Pixel, wrapMode);
                        }
                    }

                    isCuted = true;
                    recoverImage = destImage;
                    recoverAfterColorChange = destImage;
                    pictureBox1.Image = destImage;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("������������ ��������");
                }
            }
            else
            {
                MessageBox.Show("����� �������� ����������, ������� �������� ��.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)//��������� ��������� �����������
        {
            panel1.AutoScroll = true;
        }

        private void button6_Click(object sender, EventArgs e)//������ ��������.
        {
            if(isCuted)//���� ��������, �� �������� ��.
            {
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;

                pictureBox1.Image = recoverAfterCut;
                recoverAfterColorChange = pictureBox1.Image;
                isCuted = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)//������� �����
        {
            //��������� �����.
            isScrolled = false;
            isCuted = false;

            trackBar1.Value = 1;
            trackBar2.Value = 1;
            trackBar3.Value = 1;

            pictureBox1.Image = Image.FromFile(imagePath);
        }

        private void button5_Click(object sender, EventArgs e)//������� �����.
        {
            if(isScrolled & !isCuted)
            {
                trackBar1.Value = 0;
                trackBar2.Value = 0;
                trackBar3.Value = 0;

                pictureBox1.Image = firstColorReturn;

                isScrolled = false;
            }
            if(isScrolled & isCuted)
            {
                MessageBox.Show("�������� �����������, ���������� ������������ ����� �����������. ����� ��� ������� ������� ����������� � ���� �� ��������� ��� �������");
            }
            if (isScrolled)
            {
                trackBar1.Value = 0;
                trackBar2.Value = 0;
                trackBar3.Value = 0;

                pictureBox1.Image = recoverAfterColorChange;

                isScrolled = false;
            }
        }
    }
}