using FlowApp.Controllers;
using FlowApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers（デフォルト）
builder.Services.AddControllers();

// OpenAPI（デフォルト）
builder.Services.AddOpenApi();

// ▼ InMemory DB を追加
builder.Services.AddDbContext<FlowDbContext>(opt =>
    opt.UseInMemoryDatabase("FlowDb"));

var app = builder.Build();

// 静的ファイルを有効化
app.UseStaticFiles();


// OpenAPI（デフォルト）
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// HTTPS リダイレクトは Docker で邪魔になるのでオフ
// app.UseHttpsRedirection();

// 認証は使わないので削除
// app.UseAuthorization();

// ▼▼ Minimal API 追加 ▼▼

// GET 全ノード取得
app.MapGet("/flow/node", async (FlowDbContext db) =>
{
    return await db.Nodes.ToListAsync();
});

// POST ノード追加
app.MapPost("/flow/node", async (FlowDbContext db, FlowNode node) =>
{
    db.Nodes.Add(node);
    await db.SaveChangesAsync();
    return Results.Ok(node);
});

// Controllers も有効化したまま
app.MapControllers();

// アプリ起動
app.Run();
