using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace GOorselProgramlamaUI
{
    public partial class Form1 : Form
    {
        private SqlConnection conn;
        private string connString = "Server=localhost; Database=KullaniciBilgileri; Integrated Security=True;";
        public Form1()
        {
            InitializeComponent();
        }
        private void ConnectToDatabase()
        {
            conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                MessageBox.Show("Veritabanına Bağlantı Başarılı");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı Hatası: " + ex.Message);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ConnectToDatabase();
        }
        private string GetZodiacSign(DateTime birthDate, out string zodiacImage)
        {
            int day = birthDate.Day;
            int month = birthDate.Month;
            zodiacImage = "";  // Burç resmi URL'si

            if ((month == 3 && day >= 21) || (month == 4 && day <= 19))
            {
                zodiacImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3d/Aries_symbol_%28bold%29.svg/60px-Aries_symbol_%28bold%29.svg.png";
                return "Koç";
            }
            if ((month == 4 && day >= 20) || (month == 5 && day <= 20))
            {
                zodiacImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/57/Taurus_symbol_%28bold%29.svg/60px-Taurus_symbol_%28bold%29.svg.png";
                return "Boğa";
            }
            if ((month == 5 && day >= 21) || (month == 6 && day <= 20))
            {
                zodiacImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/Gemini_symbol_%28bold%29.svg/60px-Gemini_symbol_%28bold%29.svg.png";
                return "İkizler";
            }
            if ((month == 6 && day >= 21) || (month == 7 && day <= 22))
            {
                zodiacImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/38/Cancer_symbol_%28bold%29.svg/60px-Cancer_symbol_%28bold%29.svg.png";
                return "Yengeç";
            }
            if ((month == 7 && day >= 23) || (month == 8 && day <= 22))
            {
                zodiacImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/84/Leo_symbol_%28bold%29.svg/60px-Leo_symbol_%28bold%29.svg.png";
                return "Aslan";
            }
            if ((month == 8 && day >= 23) || (month == 9 && day <= 22))
            {
                zodiacImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/39/Virgo_symbol_%28bold%29.svg/60px-Virgo_symbol_%28bold%29.svg.png";
                return "Başak";
            }
            if ((month == 9 && day >= 23) || (month == 10 && day <= 22))
            {
                zodiacImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/96/Libra_symbol_%28bold%29.svg/60px-Libra_symbol_%28bold%29.svg.png";
                return "Terazi";
            }
            if ((month == 10 && day >= 23) || (month == 11 && day <= 21))
            {
                zodiacImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/ca/Scorpius_symbol_%28bold%29.svg/60px-Scorpius_symbol_%28bold%29.svg.png";
                return "Akrep";
            }
            if ((month == 11 && day >= 22) || (month == 12 && day <= 21))
            {
                zodiacImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f6/Sagittarius_symbol_%28bold%29.svg/60px-Sagittarius_symbol_%28bold%29.svg.png";
                return "Yay";
            }
            if ((month == 12 && day >= 22) || (month == 1 && day <= 19))
            {
                zodiacImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/cc/Capricornus_symbol_%28bold%29.svg/60px-Capricornus_symbol_%28bold%29.svg.png";
                return "Oğlak";
            }
            if ((month == 1 && day >= 20) || (month == 2 && day <= 18))
            {
                zodiacImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d5/Aquarius_symbol_%28bold%29.svg/60px-Aquarius_symbol_%28bold%29.svg.png";
                return "Kova";
            }
            zodiacImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d7/Pisces_symbol_%28bold%29.svg/60px-Pisces_symbol_%28bold%29.svg.png";
            return "Balık"; // default case
        }
       
        private void LoadData()
        {
            string selectQuery = "SELECT * FROM Kullanici";
            SqlCommand cmd = new SqlCommand(selectQuery, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                // Verileri almak ve formda göstermek
                string adi = reader["Adi"].ToString();
                string soyadi = reader["Soyadi"].ToString();
                DateTime dogumTarihi = DateTime.Parse(reader["DogumTarihi"].ToString());
                decimal vki = decimal.Parse(reader["Vki"].ToString());

                // UI'da gösterme işlemi
            }

            reader.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (conn == null || conn.State != ConnectionState.Open)
            {
                ConnectToDatabase();
            }

            try
            {
                string name = nameTextBox.Text;
                string surname = lastnameTextBox.Text;
                DateTime birthday = DateTime.Parse(birthdayTextBox.Text);
                decimal height = decimal.Parse(heightTextBox.Text);
                decimal weight = decimal.Parse(weightTextBox.Text);

                string zodiacSign = GetZodiacSign(birthday, out string zodiacImage);
                decimal bodyMassIndex = weight / (height * height);


                string insertQuery = "INSERT INTO Kullanici (Adi, Soyadi, DogumTarihi, Boy, Kilo, Burc, Vki, BurcResmi) " +
                             "VALUES (@Adi, @Soyadi, @DogumTarihi, @Boy, @Kilo, @Burc, @Vki, @BurcResmi)";

                // Data Save
                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@Adi", name);
                cmd.Parameters.AddWithValue("@Soyadi", surname);
                cmd.Parameters.AddWithValue("@DogumTarihi", birthday);
                cmd.Parameters.AddWithValue("@Boy", height);
                cmd.Parameters.AddWithValue("@Kilo", weight);
                cmd.Parameters.AddWithValue("@Burc", zodiacSign);
                cmd.Parameters.AddWithValue("@Vki", bodyMassIndex);
                cmd.Parameters.AddWithValue("@BurcResmi", zodiacImage);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Veri başarıyla kaydedildi!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veri kaydedilirken hata oluştu: " + ex.Message);
                }

                // UI Show
                rNameTextBox.Text = name;
                rSurnameTextBox.Text = surname;
                rHeightTextBox.Text = height.ToString();
                rWeightTextBox.Text = weight.ToString();
                bmiTextBox.Text = bodyMassIndex.ToString();
                bmiComment.Text = "";
                rBirthdayTextBox.Text = birthday.ToString("dd/MM/yyyy");
                zodiacTextBox.Text = zodiacSign;
                zodiacPictureBox.ImageLocation = zodiacImage;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri kaydedilirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bmiTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
