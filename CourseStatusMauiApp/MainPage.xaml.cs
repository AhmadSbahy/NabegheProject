using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace CourseStatusWinFormApp
{
	public partial class MainPage : ContentPage
	{
		private HttpClient client;
		private double animatedX = 0;
		// وضعیت ماشین حساب
		private string calcExpression = "";

		public MainPage()
		{
			InitializeComponent();
			client = new HttpClient { BaseAddress = new Uri("https://api.mynabeghe.ir/") };

			// تنظیم تاریخ به تقویم شمسی
			PersianCalendar pc = new PersianCalendar();
			DateTime now = DateTime.Now;
			int pYear = pc.GetYear(now);
			int pMonth = pc.GetMonth(now);
			int pDay = pc.GetDayOfMonth(now);
			lblToday.Text = $"تاریخ امروز: {pYear}/{pMonth:D2}/{pDay:D2}";

			// شروع انیمیشن پیشرفته با استفاده از متدهای MAUI
			StartAnimation();
		}

		private async void StartAnimation()
		{
			// استفاده از TranslateTo برای ایجاد انیمیشن تکراری
			while (true)
			{
				await lblAnimated.TranslateTo(0, 0, 500);
				await lblAnimated.TranslateTo(100, 0, 500);
				await lblAnimated.TranslateTo(0, 0, 500);
			}
		}

		private async void btnGetAll_Click(object sender, EventArgs e)
		{
			var filterDto = new CourseStatusFilterDto
			{
				Page = 1,
				PageCount = 0,
				AllEntitiesCount = 0,
				StartPage = 0,
				EndPage = 0,
				TakeEntity = 10,
				SkipEntity = 0,
				HowManyShowPageAfterAndBefore = 5,
				Param = "",
				FilterCourseStatusOrder = FilterCourseStatusOrder._0,
				// مقداردهی اولیه به عنوان لیست خالی
				Entities = new List<CourseStatusDto>()
			};

			try
			{
				var json = JsonConvert.SerializeObject(filterDto);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await client.PutAsync("api/CourseStatus", content);

				if (response.IsSuccessStatusCode)
				{
					var jsonResponse = await response.Content.ReadAsStringAsync();
					var courseStatusResponse = JsonConvert.DeserializeObject<CourseStatusFilterDto>(jsonResponse);

					if (courseStatusResponse != null &&
						courseStatusResponse.Entities != null &&
						courseStatusResponse.Entities.Count > 0)
					{
						listViewCourseStatus.ItemsSource = courseStatusResponse.Entities;
					}
					else
					{
						await DisplayAlert("پیام", "هیچ وضعیت دوره‌ای یافت نشد.", "باشه");
					}
				}
				else
				{
					await DisplayAlert("خطا", $"خطا در دریافت لیست: {response.StatusCode}", "باشه");
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("Exception", ex.Message, "باشه");
			}
		}

		private async void btnGetById_Click(object sender, EventArgs e)
		{
			if (int.TryParse(txtGetById.Text, out int id))
			{
				try
				{
					HttpResponseMessage response = await client.GetAsync($"api/CourseStatus/{id}");
					if (response.IsSuccessStatusCode)
					{
						var jsonResponse = await response.Content.ReadAsStringAsync();
						var courseStatus = JsonConvert.DeserializeObject<CourseStatusDto>(jsonResponse);
						if (courseStatus != null)
						{
							txtDetails.Text = courseStatus.ToString();
						}
						else
						{
							await DisplayAlert("پیام", "پاسخی دریافت نشد.", "باشه");
						}
					}
					else
					{
						await DisplayAlert("خطا", $"خطا در دریافت جزئیات: {response.StatusCode}", "باشه");
					}
				}
				catch (Exception ex)
				{
					await DisplayAlert("Exception", ex.Message, "باشه");
				}
			}
			else
			{
				await DisplayAlert("خطا", "شناسه وارد شده معتبر نیست.", "باشه");
			}
		}

		private async void btnCreate_Click(object sender, EventArgs e)
		{
			var createDto = new CreateCourseStatusDto
			{
				StatusTitle = txtCreateTitle.Text,
				CreateDate = DateTime.Now
			};

			try
			{
				var json = JsonConvert.SerializeObject(createDto);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await client.PostAsync("api/CourseStatus", content);
				if (response.IsSuccessStatusCode)
				{
					var jsonResponse = await response.Content.ReadAsStringAsync();
					var commandResponse = JsonConvert.DeserializeObject<BaseCommandResponse>(jsonResponse);
					await DisplayAlert("پیام", "وضعیت دوره با موفقیت ایجاد شد.\n" + (commandResponse?.Message ?? ""), "باشه");
				}
				else
				{
					await DisplayAlert("خطا", $"خطا در ایجاد وضعیت دوره: {response.StatusCode}", "باشه");
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("Exception", ex.Message, "باشه");
			}
		}

		private async void btnUpdate_Click(object sender, EventArgs e)
		{
			if (int.TryParse(txtUpdateId.Text, out int id))
			{
				var updateDto = new UpdateCourseStatusDto
				{
					Id = id,
					StatusTitle = txtUpdateTitle.Text,
					CreateDate = DateTime.Now
				};

				try
				{
					var json = JsonConvert.SerializeObject(updateDto);
					var content = new StringContent(json, Encoding.UTF8, "application/json");
					HttpResponseMessage response = await client.PutAsync($"api/CourseStatus/{id}", content);
					if (response.IsSuccessStatusCode)
					{
						var jsonResponse = await response.Content.ReadAsStringAsync();
						var commandResponse = JsonConvert.DeserializeObject<BaseCommandResponse>(jsonResponse);
						await DisplayAlert("پیام", "وضعیت دوره با موفقیت به‌روزرسانی شد.\n" + (commandResponse?.Message ?? ""), "باشه");
					}
					else
					{
						await DisplayAlert("خطا", $"خطا در به‌روزرسانی وضعیت دوره: {response.StatusCode}", "باشه");
					}
				}
				catch (Exception ex)
				{
					await DisplayAlert("Exception", ex.Message, "باشه");
				}
			}
			else
			{
				await DisplayAlert("خطا", "شناسه وارد شده معتبر نیست.", "باشه");
			}
		}

		private async void btnDelete_Click(object sender, EventArgs e)
		{
			if (int.TryParse(txtDeleteId.Text, out int id))
			{
				try
				{
					HttpResponseMessage response = await client.DeleteAsync($"api/CourseStatus/{id}");
					if (response.IsSuccessStatusCode)
					{
						await DisplayAlert("پیام", "وضعیت دوره با موفقیت حذف شد.", "باشه");
					}
					else
					{
						await DisplayAlert("خطا", $"خطا در حذف وضعیت دوره: {response.StatusCode}", "باشه");
					}
				}
				catch (Exception ex)
				{
					await DisplayAlert("Exception", ex.Message, "باشه");
				}
			}
			else
			{
				await DisplayAlert("خطا", "شناسه وارد شده معتبر نیست.", "باشه");
			}
		}

		// مدیریت دکمه‌های ماشین حساب
		private void CalculatorButton_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			string text = btn.Text;
			if (text == "C")
			{
				calcExpression = "";
				lblCalcDisplay.Text = "0";
			}
			else
			{
				calcExpression += text;
				lblCalcDisplay.Text = calcExpression;
			}
		}

		private void CalculatorEquals_Click(object sender, EventArgs e)
		{
			try
			{
				// ارزیابی عبارت با استفاده از DataTable.Compute (یک راه ساده)
				var dt = new System.Data.DataTable();
				var result = dt.Compute(calcExpression.Replace("÷", "/").Replace("×", "*"), "");
				lblCalcDisplay.Text = result.ToString();
				calcExpression = result.ToString();
			}
			catch (Exception)
			{
				DisplayAlert("خطا", "عبارت نامعتبر است.", "باشه");
				calcExpression = "";
				lblCalcDisplay.Text = "0";
			}
		}
	}

	#region DTOs and Converter Classes

	public class CourseStatusFilterDto
	{
		[JsonProperty("page")]
		public int Page { get; set; }
		[JsonProperty("pageCount")]
		public int PageCount { get; set; }
		[JsonProperty("allEntitiesCount")]
		public int AllEntitiesCount { get; set; }
		[JsonProperty("startPage")]
		public int StartPage { get; set; }
		[JsonProperty("endPage")]
		public int EndPage { get; set; }
		[JsonProperty("takeEntity")]
		public int TakeEntity { get; set; }
		[JsonProperty("skipEntity")]
		public int SkipEntity { get; set; }
		[JsonProperty("howManyShowPageAfterAndBefore")]
		public int HowManyShowPageAfterAndBefore { get; set; }
		[JsonProperty("entities")]
		[JsonConverter(typeof(EntitiesConverter<CourseStatusDto>))]
		public List<CourseStatusDto> Entities { get; set; }
		[JsonProperty("param")]
		public string Param { get; set; }
		[JsonProperty("filterCourseStatusOrder")]
		public FilterCourseStatusOrder FilterCourseStatusOrder { get; set; }
	}

	public enum FilterCourseStatusOrder
	{
		_0 = 0,
		_1 = 1,
	}

	public class CourseStatusDto
	{
		[JsonProperty("statusTitle")]
		public string StatusTitle { get; set; }
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("createDate")]
		public DateTime CreateDate { get; set; }
		public override string ToString()
		{
			return $"شناسه: {Id}, نام: {StatusTitle}";
		}
	}

	public class CreateCourseStatusDto
	{
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("createDate")]
		public DateTime CreateDate { get; set; }
		[JsonProperty("statusTitle")]
		public string StatusTitle { get; set; }
	}

	public class UpdateCourseStatusDto
	{
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("createDate")]
		public DateTime CreateDate { get; set; }
		[JsonProperty("statusTitle")]
		public string StatusTitle { get; set; }
	}

	public class BaseCommandResponse
	{
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("erroresList")]
		public ICollection<string> ErroresList { get; set; }
		[JsonProperty("message")]
		public string Message { get; set; }
		[JsonProperty("isSuccess")]
		public bool IsSuccess { get; set; }
	}

	public class EntitiesConverter<T> : JsonConverter<List<T>>
	{
		public override List<T> ReadJson(JsonReader reader, Type objectType, List<T> existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.StartObject)
			{
				JObject jObject = JObject.Load(reader);
				JToken valuesToken = jObject["$values"];
				if (valuesToken != null)
				{
					return valuesToken.ToObject<List<T>>(serializer);
				}
			}
			return serializer.Deserialize<List<T>>(reader);
		}
		public override void WriteJson(JsonWriter writer, List<T> value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value);
		}
	}

	#endregion
}
