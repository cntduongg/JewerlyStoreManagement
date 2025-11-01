using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Npgsql;
using System.Collections.Generic;
using System.Linq;

class Program1
{
    // ⚙️ CẤU HÌNH - SỬA LẠI CHO ĐÚNG DATABASE CỦA BẠN
    private const string ConnectionString =
        "Host=localhost;Port=5432;Database=jewelrystore;Username=postgres;Password=123456";

    // Đường dẫn tương đối từ ImageMigrationTool đến wwwroot/images/products
    private static readonly string ImageFolder =
        Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "C:\\Users\\ADMIN\\Desktop\\SWD312\\JWPublic\\wwwroot\\Images"));

    static async Task Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║   IMAGE MIGRATION TOOL - JEWELRY STORE  ║");
        Console.WriteLine("╚════════════════════════════════════════╝\n");

        // Kiểm tra folder có tồn tại không
        if (!Directory.Exists(ImageFolder))
        {
            Console.WriteLine($"❌ Folder không tồn tại: {ImageFolder}");
            Console.WriteLine("   Vui lòng tạo folder wwwroot/images/products/ trước!");
            return;
        }

        Console.WriteLine($"📁 Image folder: {ImageFolder}");
        Console.WriteLine($"🔌 Connecting to database...\n");

        var updates = new List<(int productId, string oldUrl, string newFileName)>();

        try
        {
            // Kết nối database
            using var connection = new NpgsqlConnection(ConnectionString);
            await connection.OpenAsync();
            Console.WriteLine("✅ Database connected!\n");

            // Lấy tất cả products có ảnh
            var command = new NpgsqlCommand(
                "SELECT productid, mainimageurl FROM public.products WHERE mainimageurl IS NOT NULL ORDER BY productid",
                connection
            );

            using var reader = await command.ExecuteReaderAsync();
            var products = new List<(int id, string url)>();

            while (await reader.ReadAsync())
            {
                products.Add((reader.GetInt32(0), reader.GetString(1)));
            }

            Console.WriteLine($"📦 Found {products.Count} products with images\n");
            Console.WriteLine("⏳ Starting download...\n");

            // Download từng ảnh
            using var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(30);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

            int successCount = 0;
            int failCount = 0;

            foreach (var (productId, imageUrl) in products)
            {
                try
                {
                    // Tạo tên file mới (đơn giản: 1.jpg, 2.jpg, ...)
                    string newFileName = $"{productId}.jpg";
                    string filePath = Path.Combine(ImageFolder, newFileName);

                    // Download ảnh
                    Console.Write($"   [{successCount + failCount + 1}/{products.Count}] Product {productId,-3} ");

                    var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
                    await File.WriteAllBytesAsync(filePath, imageBytes);

                    Console.WriteLine($"✅ {newFileName} ({imageBytes.Length / 1024} KB)");

                    // Lưu để update DB sau
                    updates.Add((productId, imageUrl, newFileName));
                    successCount++;

                    // Delay nhỏ để tránh bị server chặn
                    await Task.Delay(300);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Failed: {ex.Message}");
                    failCount++;
                }
            }

            // Tạo file SQL để update database
            Console.WriteLine($"\n{'─',50}");
            Console.WriteLine($"📊 SUMMARY");
            Console.WriteLine($"{'─',50}");
            Console.WriteLine($"   Total products: {products.Count}");
            Console.WriteLine($"   ✅ Success: {successCount}");
            Console.WriteLine($"   ❌ Failed: {failCount}");
            Console.WriteLine($"{'─',50}\n");

            if (updates.Count > 0)
            {
                // Tạo file SQL
                var sqlLines = new List<string>
                {
                    "-- ═══════════════════════════════════════════════════",
                    "-- IMAGE MIGRATION SQL - Generated " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    "-- ═══════════════════════════════════════════════════",
                    "",
                    "-- STEP 1: Backup original data",
                    "CREATE TABLE IF NOT EXISTS products_backup AS SELECT * FROM public.products;",
                    "",
                    "-- STEP 2: Update mainimageurl to new filenames",
                    ""
                };

                foreach (var (productId, oldUrl, newFileName) in updates)
                {
                    sqlLines.Add($"UPDATE public.products SET mainimageurl = '{newFileName}' WHERE productid = {productId};");
                    sqlLines.Add($"-- Old URL: {oldUrl}");
                    sqlLines.Add("");
                }

                sqlLines.Add("-- ═══════════════════════════════════════════════════");
                sqlLines.Add($"-- Total updates: {updates.Count}");
                sqlLines.Add("-- ═══════════════════════════════════════════════════");

                string sqlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "migration-update.sql");
                await File.WriteAllLinesAsync(sqlFilePath, sqlLines);

                Console.WriteLine($"💾 SQL file saved: {sqlFilePath}\n");

                // Preview một vài updates
                Console.WriteLine("📋 Preview updates:");
                foreach (var update in updates.Take(3))
                {
                    Console.WriteLine($"   Product {update.productId}: '{update.newFileName}'");
                }
                if (updates.Count > 3)
                {
                    Console.WriteLine($"   ... and {updates.Count - 3} more");
                }

                Console.WriteLine("\n" + new string('═', 50));
                Console.WriteLine("⚠️  NEXT STEPS:");
                Console.WriteLine(new string('═', 50));
                Console.WriteLine("1. Review file: migration-update.sql");
                Console.WriteLine("2. Backup database (already included in SQL)");
                Console.WriteLine("3. Run SQL to update database:");
                Console.WriteLine($"   psql -U postgres -d JewerlyStore -f migration-update.sql");
                Console.WriteLine("4. Update frontend code to use new paths");
                Console.WriteLine(new string('═', 50));
            }
            else
            {
                Console.WriteLine("❌ No images were downloaded successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n❌ ERROR: {ex.Message}");
            Console.WriteLine($"\nStack trace:\n{ex.StackTrace}");
        }

        Console.WriteLine("\n✨ Migration tool finished. Press any key to exit...");
        Console.ReadKey();
    }
}