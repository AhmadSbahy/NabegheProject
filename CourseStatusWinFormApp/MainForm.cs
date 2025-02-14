using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CourseStatusWinFormApp
{
	public partial class MainForm : Form
	{
		// فیلدهای اصلی
		private HttpClient client;

		private Button btnGetAll;
		private ListBox listBoxCourseStatus;

		private GroupBox groupBoxGetById;
		private TextBox txtGetById;
		private Button btnGetById;
		private TextBox txtDetails;

		private GroupBox groupBoxCreate;
		private TextBox txtCreateTitle;
		private Button btnCreate;

		private GroupBox groupBoxUpdate;
		private TextBox txtUpdateId;
		private TextBox txtUpdateTitle;
		private Button btnUpdate;

		private GroupBox groupBoxDelete;
		private TextBox txtDeleteId;
		private Button btnDelete;

		// ویژگی‌های اضافه شده:
		private Label lblToday;
		private GroupBox groupBoxCalculator;
		private TextBox txtCalcNumber1;
		private TextBox txtCalcNumber2;
		private ComboBox cmbCalcOperation;
		private Button btnCalcCompute;
		private Label lblCalcResult;
		private Timer animationTimer;
		private Label lblAnimated;

		public MainForm()
		{
			InitializeComponentT();
			client = new HttpClient
			{
				BaseAddress = new Uri("https://api.mynabeghe.ir/")
			};
		}

		private void InitializeComponentT()
		{
			// تنظیمات فرم
			this.Text = "مدیریت وضعیت دوره";
			this.Width = 1000; // افزایش اندازه فرم برای جا دادن امکانات جدید
			this.Height = 700;
			this.StartPosition = FormStartPosition.CenterScreen;

			// نمایش تاریخ امروز در گوشه سمت راست بالا
			lblToday = new Label
			{
				Text = "تاریخ امروز: " + DateTime.Now.ToString("yyyy/MM/dd"),
				Font = new Font("Tahoma", 10, FontStyle.Bold),
				AutoSize = true,
				ForeColor = Color.Blue,
				Left = 750,
				Top = 20
			};
			this.Controls.Add(lblToday);

			// --- گروه دریافت لیست وضعیت دوره ---
			btnGetAll = new Button
			{
				Text = "دریافت لیست وضعیت دوره",
				Left = 20,
				Top = 20,
				Width = 200
			};
			btnGetAll.Click += btnGetAll_Click;
			this.Controls.Add(btnGetAll);

			listBoxCourseStatus = new ListBox
			{
				Left = 20,
				Top = 60,
				Width = 740,
				Height = 150
			};
			this.Controls.Add(listBoxCourseStatus);

			// --- گروه دریافت جزئیات بر اساس شناسه ---
			groupBoxGetById = new GroupBox
			{
				Text = "دریافت جزئیات وضعیت دوره بر اساس شناسه",
				Left = 20,
				Top = 220,
				Width = 370,
				Height = 150
			};
			this.Controls.Add(groupBoxGetById);

			Label lblGetById = new Label
			{
				Text = "شناسه:",
				Left = 10,
				Top = 30,
				AutoSize = true
			};
			groupBoxGetById.Controls.Add(lblGetById);

			txtGetById = new TextBox
			{
				Left = 80,
				Top = 25,
				Width = 100
			};
			groupBoxGetById.Controls.Add(txtGetById);

			btnGetById = new Button
			{
				Text = "دریافت",
				Left = 200,
				Top = 23,
				Width = 100
			};
			btnGetById.Click += btnGetById_Click;
			groupBoxGetById.Controls.Add(btnGetById);

			txtDetails = new TextBox
			{
				Left = 10,
				Top = 60,
				Width = 340,
				Height = 70,
				Multiline = true,
				ReadOnly = true
			};
			groupBoxGetById.Controls.Add(txtDetails);

			// --- گروه ایجاد وضعیت دوره جدید ---
			groupBoxCreate = new GroupBox
			{
				Text = "ایجاد وضعیت دوره جدید",
				Left = 410,
				Top = 220,
				Width = 350,
				Height = 100
			};
			this.Controls.Add(groupBoxCreate);

			Label lblCreateTitle = new Label
			{
				Text = "نام:",
				Left = 10,
				Top = 30,
				AutoSize = true
			};
			groupBoxCreate.Controls.Add(lblCreateTitle);

			txtCreateTitle = new TextBox
			{
				Left = 60,
				Top = 25,
				Width = 200
			};
			groupBoxCreate.Controls.Add(txtCreateTitle);

			btnCreate = new Button
			{
				Text = "ایجاد",
				Left = 270,
				Top = 23,
				Width = 60
			};
			btnCreate.Click += btnCreate_Click;
			groupBoxCreate.Controls.Add(btnCreate);

			// --- گروه به‌روزرسانی وضعیت دوره ---
			groupBoxUpdate = new GroupBox
			{
				Text = "به‌روزرسانی وضعیت دوره موجود",
				Left = 20,
				Top = 380,
				Width = 370,
				Height = 100
			};
			this.Controls.Add(groupBoxUpdate);

			Label lblUpdateId = new Label
			{
				Text = "شناسه:",
				Left = 10,
				Top = 30,
				AutoSize = true
			};
			groupBoxUpdate.Controls.Add(lblUpdateId);

			txtUpdateId = new TextBox
			{
				Left = 70,
				Top = 25,
				Width = 100
			};
			groupBoxUpdate.Controls.Add(txtUpdateId);

			Label lblUpdateTitle = new Label
			{
				Text = "نام جدید:",
				Left = 180,
				Top = 30,
				AutoSize = true
			};
			groupBoxUpdate.Controls.Add(lblUpdateTitle);

			txtUpdateTitle = new TextBox
			{
				Left = 260,
				Top = 25,
				Width = 100
			};
			groupBoxUpdate.Controls.Add(txtUpdateTitle);

			btnUpdate = new Button
			{
				Text = "ویرایش",
				Left = 10,
				Top = 60,
				Width = 100
			};
			btnUpdate.Click += btnUpdate_Click;
			groupBoxUpdate.Controls.Add(btnUpdate);

			// --- گروه حذف وضعیت دوره ---
			groupBoxDelete = new GroupBox
			{
				Text = "حذف وضعیت دوره",
				Left = 410,
				Top = 380,
				Width = 350,
				Height = 100
			};
			this.Controls.Add(groupBoxDelete);

			Label lblDeleteId = new Label
			{
				Text = "شناسه:",
				Left = 10,
				Top = 30,
				AutoSize = true
			};
			groupBoxDelete.Controls.Add(lblDeleteId);

			txtDeleteId = new TextBox
			{
				Left = 60,
				Top = 25,
				Width = 100
			};
			groupBoxDelete.Controls.Add(txtDeleteId);

			btnDelete = new Button
			{
				Text = "حذف",
				Left = 170,
				Top = 23,
				Width = 100
			};
			btnDelete.Click += btnDelete_Click;
			groupBoxDelete.Controls.Add(btnDelete);

			// --- گروه ماشین حساب ساده ---
			groupBoxCalculator = new GroupBox
			{
				Text = "ماشین حساب ساده",
				Left = 20,
				Top = 500,
				Width = 350,
				Height = 150
			};
			this.Controls.Add(groupBoxCalculator);

			Label lblNumber1 = new Label
			{
				Text = "عدد اول:",
				Left = 10,
				Top = 30,
				AutoSize = true
			};
			groupBoxCalculator.Controls.Add(lblNumber1);

			txtCalcNumber1 = new TextBox
			{
				Left = 80,
				Top = 25,
				Width = 100
			};
			groupBoxCalculator.Controls.Add(txtCalcNumber1);

			Label lblNumber2 = new Label
			{
				Text = "عدد دوم:",
				Left = 10,
				Top = 70,
				AutoSize = true
			};
			groupBoxCalculator.Controls.Add(lblNumber2);

			txtCalcNumber2 = new TextBox
			{
				Left = 80,
				Top = 65,
				Width = 100
			};
			groupBoxCalculator.Controls.Add(txtCalcNumber2);

			cmbCalcOperation = new ComboBox
			{
				Left = 200,
				Top = 25,
				Width = 100,
				DropDownStyle = ComboBoxStyle.DropDownList
			};
			cmbCalcOperation.Items.AddRange(new string[] { "+", "-", "*", "/" });
			cmbCalcOperation.SelectedIndex = 0;
			groupBoxCalculator.Controls.Add(cmbCalcOperation);

			btnCalcCompute = new Button
			{
				Text = "محاسبه",
				Left = 200,
				Top = 65,
				Width = 100
			};
			btnCalcCompute.Click += BtnCalcCompute_Click;
			groupBoxCalculator.Controls.Add(btnCalcCompute);

			lblCalcResult = new Label
			{
				Text = "نتیجه:",
				Left = 10,
				Top = 110,
				AutoSize = true,
				Font = new Font("Tahoma", 10, FontStyle.Bold),
				ForeColor = Color.DarkGreen
			};
			groupBoxCalculator.Controls.Add(lblCalcResult);

			// --- انیمیشن: یک label متحرک ---
			lblAnimated = new Label
			{
				Text = "به دنیای برنامه نویسی خوش آمدید!",
				AutoSize = true,
				Font = new Font("Tahoma", 12, FontStyle.Italic),
				ForeColor = Color.Purple,
				Left = 400,
				Top = 550
			};
			this.Controls.Add(lblAnimated);

			animationTimer = new Timer
			{
				Interval = 50 // به ازای هر 50 میلی‌ثانیه
			};
			animationTimer.Tick += AnimationTimer_Tick;
			animationTimer.Start();
		}

		private void AnimationTimer_Tick(object sender, EventArgs e)
		{
			// حرکت label به صورت افقی
			lblAnimated.Left += 5;
			if (lblAnimated.Left > this.Width)
			{
				lblAnimated.Left = -lblAnimated.Width;
			}
		}

		private void BtnCalcCompute_Click(object sender, EventArgs e)
		{
			if (double.TryParse(txtCalcNumber1.Text, out double num1) &&
				double.TryParse(txtCalcNumber2.Text, out double num2))
			{
				double result = 0;
				switch (cmbCalcOperation.SelectedItem.ToString())
				{
					case "+":
						result = num1 + num2;
						break;
					case "-":
						result = num1 - num2;
						break;
					case "*":
						result = num1 * num2;
						break;
					case "/":
						if (num2 != 0)
							result = num1 / num2;
						else
						{
							MessageBox.Show("تقسیم بر صفر امکان پذیر نیست.");
							return;
						}
						break;
				}
				lblCalcResult.Text = "نتیجه: " + result.ToString();
			}
			else
			{
				MessageBox.Show("لطفاً اعداد معتبر وارد کنید.");
			}
		}

		// متدهای قبلی مانند btnGetAll_Click، btnGetById_Click، btnCreate_Click، btnUpdate_Click و btnDelete_Click
		// بدون تغییر در ادامه آورده شده‌اند:

		private async void btnGetAll_Click(object sender, EventArgs e)
		{
			var filterDto = new CourseStatusFilterDto()
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
				Entities = null
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

					listBoxCourseStatus.Items.Clear();
					if (courseStatusResponse != null &&
						courseStatusResponse.Entities != null &&
						courseStatusResponse.Entities.Count > 0)
					{
						foreach (var cs in courseStatusResponse.Entities)
						{
							listBoxCourseStatus.Items.Add(cs.ToString());
						}
					}
					else
					{
						MessageBox.Show("هیچ وضعیت دوره‌ای یافت نشد.");
					}
				}
				else
				{
					MessageBox.Show($"خطا در دریافت لیست: {response.StatusCode}");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception: " + ex.Message);
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
						var courseStatus = await response.Content.ReadFromJsonAsync<CourseStatusDto>();
						if (courseStatus != null)
						{
							txtDetails.Text = courseStatus.ToString();
						}
						else
						{
							MessageBox.Show("پاسخی دریافت نشد.");
						}
					}
					else
					{
						MessageBox.Show($"خطا در دریافت جزئیات: {response.StatusCode}");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Exception: " + ex.Message);
				}
			}
			else
			{
				MessageBox.Show("شناسه وارد شده معتبر نیست.");
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
				HttpResponseMessage response = await client.PostAsJsonAsync("api/CourseStatus", createDto);
				if (response.IsSuccessStatusCode)
				{
					var commandResponse = await response.Content.ReadFromJsonAsync<BaseCommandResponse>();
					MessageBox.Show("وضعیت دوره با موفقیت ایجاد شد.\n" + (commandResponse?.Message ?? ""));
				}
				else
				{
					MessageBox.Show($"خطا در ایجاد وضعیت دوره: {response.StatusCode}");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception: " + ex.Message);
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
					HttpResponseMessage response = await client.PutAsJsonAsync($"api/CourseStatus/{id}", updateDto);
					if (response.IsSuccessStatusCode)
					{
						var commandResponse = await response.Content.ReadFromJsonAsync<BaseCommandResponse>();
						MessageBox.Show("وضعیت دوره با موفقیت به‌روزرسانی شد.\n" + (commandResponse?.Message ?? ""));
					}
					else
					{
						MessageBox.Show($"خطا در به‌روزرسانی وضعیت دوره: {response.StatusCode}");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Exception: " + ex.Message);
				}
			}
			else
			{
				MessageBox.Show("شناسه وارد شده معتبر نیست.");
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
						MessageBox.Show("وضعیت دوره با موفقیت حذف شد.");
					}
					else
					{
						MessageBox.Show($"خطا در حذف وضعیت دوره: {response.StatusCode}");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Exception: " + ex.Message);
				}
			}
			else
			{
				MessageBox.Show("شناسه وارد شده معتبر نیست.");
			}
		}
	}

	// کلاس‌های DTO و Converter همانند نسخه قبلی هستند:
	public class CourseStatusFilterDto
	{
		[JsonProperty("page", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int Page { get; set; }

		[JsonProperty("pageCount", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int PageCount { get; set; }

		[JsonProperty("allEntitiesCount", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int AllEntitiesCount { get; set; }

		[JsonProperty("startPage", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int StartPage { get; set; }

		[JsonProperty("endPage", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int EndPage { get; set; }

		[JsonProperty("takeEntity", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int TakeEntity { get; set; }

		[JsonProperty("skipEntity", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int SkipEntity { get; set; }

		[JsonProperty("howManyShowPageAfterAndBefore", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int HowManyShowPageAfterAndBefore { get; set; }

		[JsonProperty("entities", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		[JsonConverter(typeof(EntitiesConverter<CourseStatusDto>))]
		public List<CourseStatusDto> Entities { get; set; }

		[JsonProperty("param", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string Param { get; set; }

		[JsonProperty("filterCourseStatusOrder", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
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
		[JsonProperty("id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int Id { get; set; }

		[JsonProperty("createDate", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public DateTime CreateDate { get; set; }

		[JsonProperty("statusTitle", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string StatusTitle { get; set; }
	}

	public class UpdateCourseStatusDto
	{
		[JsonProperty("id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int Id { get; set; }

		[JsonProperty("createDate", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public DateTime CreateDate { get; set; }

		[JsonProperty("statusTitle", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string StatusTitle { get; set; }
	}

	public class BaseCommandResponse
	{
		[JsonProperty("id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int Id { get; set; }

		[JsonProperty("erroresList", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public ICollection<string> ErroresList { get; set; }

		[JsonProperty("message", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string Message { get; set; }

		[JsonProperty("isSuccess", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
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
}
