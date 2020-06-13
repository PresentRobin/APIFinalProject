using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;

//640df12f0385819367d2c9dbbb3fe76a
//542ffd081e67f4512b705f89d2a611b2

namespace App_UR_wardrobe
{
    public partial class weatherAPI_form : Form
    {
        string city;
        public weatherAPI_form()
        {
            InitializeComponent();
        }

        private void weatherAPI_Load(object sender, EventArgs e)
        {

        }

        void getWeather(string city)
        {
            const string APPID = "640df12f0385819367d2c9dbbb3fe76a";
            using (WebClient web = new WebClient())
            {
                string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric&cnt=6", city, APPID);

                var json = web.DownloadString(url);

                var result = JsonConvert.DeserializeObject<weatherInfo.root>(json);

                weatherInfo.root outPut = result;

                lbl_cityName.Text = string.Format("{0}", outPut.name);
                lbl_country.Text = string.Format("{0}", outPut.sys.country);
                lbl_temp.Text = string.Format("{0} \u00B0" + "C", Math.Round(outPut.main.temp));
            }
        }
        void getForcast(string city)
        {
            const string APPID2 = "542ffd081e67f4512b705f89d2a611b2";
            int day = 5;
            string url = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&cnt={1}&appid={2}", city, day, APPID2);
            using (WebClient web = new WebClient())
            {
                var json = web.DownloadString(url);
                var Object = JsonConvert.DeserializeObject<weatherForcast>(json);

                weatherForcast forcast = Object;

                //tomorrow
                days_2.Text = string.Format("{0}", getDate(forcast.list[1].dt).DayOfWeek);
                lbl_cont_2.Text = string.Format("{0}", forcast.list[1].weather[0].main);
                lbl_desc_2.Text = string.Format("{0}", forcast.list[1].weather[0].description);
                lbl_temp_2.Text = string.Format("{0} \u00B0" + "C", Math.Round(forcast.list[1].temp.day));
                lbl_wind_2.Text = string.Format("{0} m/s", forcast.list[1].speed);

                //day after tomorrow
                days_3.Text = string.Format("{0}", getDate(forcast.list[2].dt).DayOfWeek);
                lbl_cont_3.Text = string.Format("{0}", forcast.list[2].weather[0].main);
                lbl_desc_3.Text = string.Format("{0}", forcast.list[2].weather[0].description);
                lbl_temp_3.Text = string.Format("{0} \u00B0" + "C", Math.Round(forcast.list[2].temp.day));
                lbl_wind_3.Text = string.Format("{0} m/s", forcast.list[2].speed);

                //day after tomorrow
                days_4.Text = string.Format("{0}", getDate(forcast.list[3].dt).DayOfWeek);
                lbl_cont_4.Text = string.Format("{0}", forcast.list[3].weather[0].main);
                lbl_desc_4.Text = string.Format("{0}", forcast.list[3].weather[0].description);
                lbl_temp_4.Text = string.Format("{0} \u00B0" + "C", Math.Round(forcast.list[3].temp.day));
                lbl_wind_4.Text = string.Format("{0} m/s", forcast.list[3].speed);

                //day after tomorrow
                days_5.Text = string.Format("{0}", getDate(forcast.list[4].dt).DayOfWeek);
                lbl_cont_5.Text = string.Format("{0}", forcast.list[4].weather[0].main);
                lbl_desc_5.Text = string.Format("{0}", forcast.list[4].weather[0].description);
                lbl_temp_5.Text = string.Format("{0} \u00B0" + "C", Math.Round(forcast.list[4].temp.day));
                lbl_wind_5.Text = string.Format("{0} m/s", forcast.list[4].speed);

                //day after tomorrow
                /*days_6.Text = string.Format("{0}", getDate(forcast.list[5].dt).DayOfWeek);
                lbl_cont_6.Text = string.Format("{0}", forcast.list[5].weather[0].main);
                lbl_desc_6.Text = string.Format("{0}", forcast.list[5].weather[0].description);
                lbl_temp_6.Text = string.Format("{0} \u00B0" + "C", forcast.list[5].temp.day);
                lbl_wind_6.Text = string.Format("{0} m/s", forcast.list[5].speed);*/

            }
        }
        DateTime getDate(double milliesecound)
        {
            DateTime day = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(milliesecound).ToLocalTime();

            return day;
        }

        private void textBox_enter_TextChanged(object sender, EventArgs e)
        {
        }

        private void submit_location_Click(object sender, EventArgs e)
        {
            city = textBox_enter.Text;

            getWeather(city); //local weather
            getForcast(city); //more than one day
        }
    }
}