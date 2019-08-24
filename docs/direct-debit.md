- [مقدمه](#%مقدمه)
- [تعریف قانون برداشت](#%d8%aa%d8%b9%d8%b1%db%8c%d9%81-%d9%82%d8%a7%d9%86%d9%88%d9%86-%d8%a8%d8%b1%d8%af%d8%a7%d8%b4%d8%aa)
- [دریافت مجوز از کاربر](#%d8%af%d8%b1%db%8c%d8%a7%d9%81%d8%aa-%d9%85%d8%ac%d9%88%d8%b2-%d8%a7%d8%b2-%da%a9%d8%a7%d8%b1%d8%a8%d8%b1)
- [تمدید عضویت](#%d8%aa%d9%85%d8%af%db%8c%d8%af-%d8%b9%d8%b6%d9%88%db%8c%d8%aa)
- [دانلود نمونه کد‌ها](#%d8%af%d8%a7%d9%86%d9%84%d9%88%d8%af-%d9%86%d9%85%d9%88%d9%86%d9%87-%da%a9%d8%af%e2%80%8c%d9%87%d8%a7)


## مقدمه

در صورتی که در برنامه خود در نظر دارید که کاربران را عضو سرویسی نمایید و بتوانید از کیف پول کاربران خود در ازای عضویت در بازه‌های مختلف برداشت نمایید، می توانید از قابلیت **مجوز برداشت مستقیم** استفاده نمایید.
مزیت برداشت مستقیم این است که دیگر برای هر فاکتوری که تولید می کنید نیازی نیست که کاربر را به صفحه پرداخت هدایت کنید تا تایید او را دریافت کنید. بلکه شما پس از نمایش قانون برداشت مستقیم به کاربر، یک‌بار از کاربر مجوز برداشت خود را از کاربر می گیرید و تا زمانی که قانونی که کاربر تایید کرده امکان برداشت را به شما بدهد می‌توانید از کیف پول او برداشت نمایید.

برای استفاده از این قابلیت، ابتدا شما قانونی برای مجوز خودتان تعیین می کنید. در این قانون شما تعیین می کنید که مایلید چندبار امکان برداشت از حساب کاربر داشته باشید و یا اینکه این قانون تا چند روز معتبر است. همینطور سقف مبلغ برداشت را نیز می‌توانید تعیین کنید. برای مثال این قوانین را در نظر بگیرید:

- بتوان برای ۵ روز آتی، ۵ بار از حساب کاربر برداشت شود
- بتوان برای ۱۰ روز بدون محدودیت به هر تعدادی که مایل بودیم برداشت شود.
- بتوان برای مدت ۲۰ روز و تا سقف ۲۰ هزار تومان و حداکثر ۴۰ بار از حساب کاربر برداشت شود.  

<br/>
  
پس از تعیین قانون برداشت، شما نیاز به دریافت ‍‍‍‍**مجوز** از کاربر دارید.
برای این کار شما ابتدا باید کاربر را به صفحه تایید مجوز هدایت نمایید تا کاربر قانون برداشت شما را مشاهده نماید. کاربر قانون برداشت را مشاهده می‌کند و درصورتی که با قانون برداشت موافق بود، آن را تایید می کند.
در ادامه کلیه جزئیات این قابلیت به همراه نمونه‌کد و تصاویر مربوطه نمایش داده میشود.

<div class="box-end">
</div>

## تعریف قانون برداشت

برای تعیین قانون باید نوع آن را مشخص کنید:

<div class="tab-start">
</div>

# [C#](#tab/csharp)

``` csharp
public static class WithdrawRuleTypeCodes
{
    public const string SubscriptionAmount = "WITHDRAW_RULE_TYPE_SUBSCRIPTION_AMOUNT";
    public const string Amount = "WITHDRAW_RULE_TYPE_AMOUNT";
}

```

<div class="tab-end">
</div>

در صورتی که میخواهید کاربر را به صورت موقت عضو سرویس خود نمایید می توانید نوع قانون خود را **WITHDRAW_RULE_TYPE_SUBSCRIPTION_AMOUNT** انتخاب نمایید. 
در صورتی که میخواهید کاربر به صورت دائم عضو سرویس شما شود می توانید نوع عضویت را  **WITHDRAW_RULE_TYPE_AMOUNT** انتخاب نمایید. قطعه کد زیر نحوه تعریف قانون فارغ از عضویت دائم یا موقت را نشان میدهد


<div class="tab-start">
</div>
 
# [C#](#tab/csharp)

``` csharp

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FanapPlus.Torange.Samples.Web.Clients.Exceptions;
using FanapPlus.Torange.Samples.Web.Clients.Pod.Models;
using FanapPlus.Torange.Samples.Web.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FanapPlus.Torange.Samples.Web.Clients.Pod
{
    public class PodClient : IPodClient
    {
        public const string PodHttpClientName = "PodHttpClient";
        private readonly IOptions<PodConfig> _options;
        private readonly IHttpClientFactory _clientFactory;

        public PodClient(IOptions<PodConfig> options, IHttpClientFactory clientFactory)
        {
            _options = options;
            _clientFactory = clientFactory;
        }

        private HttpClient CreateClient()
        {
            return _clientFactory.CreateClient(PodHttpClientName);
        }

        public async Task<AddWithdrawRulePlanResponse> AddWithdrawRulePlanAsync(AddWithdrawRulePlanRequest request)
        {
            HttpClient client = CreateClient();

            var url = $"{_options.Value.ApiBaseAddress}/nzh/biz/addWithdrawRulePlan";
            using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, url))
            {

                httpRequest.Headers.Add("_token_", new List<string>() { _options.Value.Token });
                httpRequest.Headers.Add("_token_issuer_", new List<string>() { "1" });

                var parameters = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("subscriptionDays", request.SubscriptionDays + ""),
                    new KeyValuePair<string, string>("maxAmount", request.MaxAmount + ""),
                    new KeyValuePair<string, string>("maxCount", request.MaxCount + ""),
                    new KeyValuePair<string, string>("typeCode", request.TypeCode + ""),
                    new KeyValuePair<string, string>("businessId", _options.Value.BusinessId + "")
                };
                httpRequest.Content = new FormUrlEncodedContent(parameters);

                var httpResponse = await client.SendAsync(httpRequest);

                var body = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    //Handle errors
                    throw new ApiException($"An error occurred: ${body}");
                }

                var result = JsonConvert.DeserializeObject<PodResult<AddWithdrawRulePlanResponse>>(body);
                if (result.HasError)
                {
                    throw new ApiException($"An error occurred: ${body}");
                }

                return result.Result;
            }
        }
    }
}


```
<div class="swaggerLink">http://sandbox.pod.land:8080/apidocs/swagger-ui.html?srv=/nzh/biz/withdrawRuleList</div>

<div class="tab-end">
</div>

مقادیر subscriptionDays، maxAmount، maxCount، typeCode، businessId به ترتیت، شناسه کسب و کار شما، حداکثر مبلغ مجاز که کسب و کار می تواند از حساب کاربر برداشت کند، حداکثر تعداد دفعاتی که کسب و کار مجاز به برداشت از حساب کاربر است، نوع قانون که در بالا توضیح داده شد و شناسه کسب و کار شما هستند.

حال شما می توانید با قطعه کد زیر قانونی برای عضویت دائم تعریف نمایید:

<div class="tab-start">
</div>

# [C#](#tab/csharp)

``` csharp

var result = await _podClient.AddWithdrawRulePlanAsync(new AddWithdrawRulePlanRequest
                {
                    MaxAmount = 100000,
                    MaxCount = 0,
                    SubscriptionDays = 0,
                    TypeCode = WithdrawRuleTypeCodes.Amount
                });

```
<div class="tab-end">
</div>

همینطور برای تعریف قانون عضویت موقت می توانید از قطعه کد زیر استفاده نمایید:

<div class="tab-end">
</div>
<div class="tab-start">
</div>

# [C#](#tab/csharp)

``` csharp

var result = await _podClient.AddWithdrawRulePlanAsync(new AddWithdrawRulePlanRequest
                {
                    MaxAmount = 100000,
                    MaxCount = 0,
                    SubscriptionDays = 10,
                    TypeCode = WithdrawRuleTypeCodes.SubscriptionAmount
                });

```

<div class="tab-end">
</div>

دقت کنید که در متغیر result مقدار id شناسه قانون برداشت است که با planId در قسمت بعد استفاده می شود.
بعد از تعریف قانون برداشت، نحوه دریافت مجوز از کاربر (یعنی عضویت دائمی یا موقت) تفاوتی ندارد.

<div class="box-end">
</div>

## دریافت مجوز از کاربر

برای دریافت مجوز شما ابتدا کاربر را باید به صفحه‌ای مخصوص در پاد هدایت نمایید تا کاربر بتواند قانون برداشتی که در مرحله قبل تعریف کردید را مشاهده نماید. سپس کاربر قانون را مشاهده میکند و در صورتی که قانون را تایید کند مجوز صادر می شود. در زمان هدایت کاربر، شما یک آدرس از سایت خودتان را هم به عنوان پارامتر برای پاد ارسال می‌کنید تا پاد بتواند کاربر را پس از دریافت مجوز به آن صفحه هدایت کند.
ممکن است کابر بعد از هدایت به پاد نیاز به لاگین کردن داشته باشد که پس از لاگین مجددا توسط پاد به صفحه دریافت مجوز هدایت می‌شود.
 قطعه کد زیر نحوه ارسال کاربر را به پاد را شامل می‌شود:

<div class="tab-start">
</div>

# [C#](#tab/csharp)

``` csharp

[HttpGet("issue")]
public async Task<IActionResult> GoToIssuePageAsync()
{
    //Get plan id from your configuration or database.
    var planId = await _databaseLayer.GetSubscriptionPlanIdAsync();

    var podUrl = $"{_podOptions.Value.WebBaseAddress}/v1/pbc/issueWithdrawRuleByPlan/" +
                    $"?businessId={_podOptions.Value.BusinessId}" +
                    $"&planId={planId}" +
                    "&redirectUri=" + 
                    //Store some information in url so we can restore required information to store issue result for user.
                    WebUtility.UrlEncode($"{Request.Scheme}://{Request.Host}/api/withdraw/issue/redirect"
                    );
    return Redirect(podUrl);
}

```

<div class="tab-end">
</div>

همان‌ طور که مشاهده می‌شود ابتدا قانونی که مدنظرمان هست را از دیتابیس پیدا می‌کنیم. سپس باید یک URL بسازیم که دارای پارامتر‌های زیر است:

- businessId: شناسه کسب و کار شما که در پنل قابل دسترسی است
- planId شناسه قانون برداشت (می‌تواند قانون برداشت دائمی یا موقت باشد)
- redirectUri آدرسی که پس از دریافت تایید از کاربر، پاد کاربر را به آن هدایت می کند. در واقع پارامتر redirectUri آدرسی از سایت شماست که می‌خواهید نتیجه مجوز دادن کاربر را در آن بررسی کنید.

<br/>

در انتها ما کاربر را به پاد هدایت می کنیم. که اطلاعاتی مشابه اطلاعات زیر به کاربر نمایش داده میشود:

<img src="http://s8.picofile.com/file/8367128884/pod_issue_confirm.JPG" alt="drawing" width="1000" height="500"/>

ما در تصویر بالا برای عضویت موقت ۳۰ روزه کاربر قانون برداشت تعیین کردیم اما همانطور که قبلا مشخص شده بود، نحوه مجوز دادن کاربر برای عضویت دائمی و یا موقت یکسان است.
کاربر با مشاهده قانون برداشت، می تواند بر روی تایید یا بازگشت کلیک کند. در صورت تایید تصویر زیر را مشاهده می کند:

<img src="http://s9.picofile.com/file/8367153792/issue_successful.JPG" alt="drawing" width="1000" height="350"/>

کاربر بر روی بازگشت کلیک می کند و به آدرسی که در پارامتر redirectUri انتخاب کرده بود، هدایت میشود.


با دریافت این مجوز، کسب و کار می تواند بدون نیاز به تایید کاربر تا سقف ۱۰۰ ریال و به مدت ۳۰ روز به هر تعداد (با رعایت سقف مبلغ)‌ از حساب کاربر برداشت کند. برای این‌ کار شما می‌توانید بخش صدور فاکتور را مشاهده نمایید. صدور فاکتور مثل قبل است اما پس از صدور شما می توانید‌ بدون تایید کاربر مبلغ را از حساب کاربر برداشت نمایید و فاکتور را ببندید.

در برداشت از حساب این امکان وجود دارد که کاربر موجودی نداشته باشد، در این صورت اگر کاربر حساب بانکی خود را در پاد تعریف کرده باشد برداشت به صورت خودکار از حساب بانکی صورت می گیرد.


<div class="box-end">
</div>

## تمدید عضویت

در صورتی که دوره عضویت موقت کاربر شما به اتمام رسیده است شما می‌توانید دوباره فرآیند دریافت مجوز را که در بالا توضیح داده شد انجام بدید. اما ابتدا باید  لیست کاربرانی که قبلا مجوز برداشت مستقیم را به کسب و کار شما اعطا کرده‌اند را بدست بیاورید. برای این منظور می توانید از API دریافت لیست مجوز‌ها که برای همین منظور تعبیه شده است استفاده کنید. این API این اجازه را می‌دهد که بتوانید مجوز را برای یک‌کاربر محدود کنید تا در صورتی که مایل بودید تنها عضویت همان کاربر را تمدید کنید. در زیر نمونه‌کد مربوط به دریافت لیست مجوز‌ها را مشاهده می‌نمایید:

<div class="tab-start">
</div>

# [C#](#tab/csharp)

``` csharp

public async Task<List<Rule>> GetRulesAsync(GetRulesRequest request)
{
    HttpClient client = CreateClient();

    var url = $"{_options.Value.ApiBaseAddress}/nzh/biz/withdrawRuleList?userId={request.UserId}" +
        $"&offset=0&size=50";

    using (var httpRequest = new HttpRequestMessage(HttpMethod.Get, url))
    {
        httpRequest.Headers.Add("_token_", new List<string>() { _options.Value.Token });
        httpRequest.Headers.Add("_token_issuer_", new List<string>() { "1" });

        var httpResponse = await client.SendAsync(httpRequest);

        var body = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new ApiException($"An error occurred: ${body}");
        }

        var result = JsonConvert.DeserializeObject<PodResult<List<Rule>>>(body);
        if (result.HasError)
        {
            return new List<Rule>();
        }

        return result.Result;
    }
}

```

<div class="swaggerLink">http://sandbox.pod.land:8080/apidocs/swagger-ui.html?srv=/nzh/biz/withdrawRuleList</div>

<div class="tab-end">
</div>


در صورتی که کاربر خاصی را در نظر دارید می توانید آن را با پارامتر userId فیلتر نمایید. همینطور امکان صفحه بندی نیز برای شما با پارامتر‌های offset و size فراهم شده است.
همانطور که در قطعه کد مشاهده میشود لیستی از Rule ها که معادل یک مجوز برای کاربر است، به شما برگردانده می‌شود. 
 فیلد‌های Rule را در زیر مشاهده می‌نمایید:


<div class="tab-start">
</div>

# [C#](#tab/csharp)

``` csharp

public class Rule
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("creationDate")]
    [JsonConverter(typeof(MicrosecondEpochConverter))]
    public DateTime CreationDate { get; set; }

    [JsonProperty("startDate")]
    [JsonConverter(typeof(MicrosecondEpochConverter))]
    public DateTime StartDate { get; set; }

    [JsonProperty("expireDate")]
    [JsonConverter(typeof(MicrosecondEpochConverter))]
    public DateTime ExpireDate { get; set; }

    [JsonProperty("maxAmount")]
    public int MaxAmount { get; set; }

    [JsonProperty("maxCount")]
    public int MaxCount { get; set; }

    [JsonProperty("typeCode")]
    public string TypeCode { get; set; }

    [JsonProperty("usageCount")]
    public int UsageCount { get; set; }

    [JsonProperty("usageAmount")]
    public int UsageAmount { get; set; }

    [JsonProperty("wallet")]
    public string Wallet { get; set; }

    [JsonProperty("expired")]
    public bool Expired { get; set; }

    [JsonProperty("user")]
    public User User { get; set; }
}

```

‍‍‍<div class="tab-end">
</div>

 هر Rule یه فیلد به اسم ExpireDate دارد. در صورتی که مقدار این فیلد کوچتر از زمان حال بود آن Rule می‌تواند تمدید شود.
قطعه‌کد زیر نحوه تمدید عضویت را نشان می‌دهد:

<div class="tab-start">
</div>

# [C#](#tab/csharp)

``` csharp

var rules = await _podClient.GetRulesAsync(new GetRulesRequest { UserId = _session.CurrentUserId });
var rule = rules.FirstOrDefault();

if (rule.ExpireDate < DateTime.UtcNow)
{
    //Redirect user to Pod confirmation page.
}
else
{

}

```

<div class="tab-end">
</div>


<div class="box-end">
</div>

## دانلود نمونه کد‌ها

شما می توانید نمونه کد‌ها را از داخل لینک زیر دانلود نمایید:

[دانلود نمونه کد](https://github.com/podiumir/samples/raw/master/plan.zip)


<div class="box-end">
</div>
