using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

// 1. יוצרים את ה"בנאי" של שרת האינטרנט שלנו
var builder = WebApplication.CreateBuilder(args);

// 2. אומרים לשרת: "תכיר, יש לנו Controllers בפרויקט שצריך להפעיל"
builder.Services.AddControllers();

// 3. בונים את האפליקציה
var app = builder.Build();

// 4. ממפים את הכתובות (כדי שהשרת ידע לנתב את http://.../api/game ל-GameController)
app.MapControllers();

// 5. מפעילים את השרת! (מרגע זה התוכנית מאזינה לבקשות ולא נסגרת)
app.Run("http://localhost:5114");