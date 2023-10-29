using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace towadrink
{
    internal class Program
    {
        private static void Main()
        {
            // 在此處初始化兩個清單，一個用於存儲飲品，另一個用於存儲訂單項目。
            List<Drink> drinks = new List<Drink>();// 初始化飲品清單
            List<OrderItem> orders = new List<OrderItem>(); // 初始化訂單清單

            // 添加飲品到飲品清單
            add_drinks(drinks);

            // 顯示飲品清單
            show_drinks(drinks);

            // 開始訂購流程
            orderdrink(drinks, orders);
        }

        private static void CalculateOrder(List<OrderItem> orders)
        {
            double total = 0.0;  // 初始化總價
            string message = "";  // 初始化訊息
            double sellPrice = 0.0;  // 初始化銷售價格

            Console.WriteLine("----------------------------------------------------------------------------------------");
            // 在此處開始計算訂單和設定相關訊息。

            // 計算訂單總金額
            foreach (OrderItem orderItem in orders)
            {
                total += orderItem.Subtotal;
            }

            // 根據總金額設定折扣和銷售價格
            if (total >= 500)
            {
                message = "訂購滿500元以上者8折";
                sellPrice = total * 0.8; // 計算 80% 折扣後的銷售價格
            }
            else if (total >= 300)
            {
                message = "訂購滿300元以上者85折";
                sellPrice = total * 0.85; // 計算 85% 折扣後的銷售價格
            }
            else if (total >= 200)
            {
                message = "訂購滿200元以上者9折";
                sellPrice = total * 0.9; // 計算 90% 折扣後的銷售價格
            }
            else
            {
                message = "訂購未滿200元不打折";
                sellPrice = total; // 未達到上述條件，不打折，銷售價格為原始總金額
            }

            // 顯示訂單總結和價格
            Console.WriteLine($"\n您總共訂購{orders.Count}項飲料 總計{total}元。{message} 總計需付款{sellPrice}元.");
            Console.WriteLine("----------------------------------------------------------------------------------------");
        }

        private static void orderdrink(List<Drink> drinks, List<OrderItem> orders)
        {
            // 提示用戶開始訂購，並告知如何結束訂購
            Console.WriteLine("\n請開始訂購，按下X鍵結束");
            string s; //宣告一個字串變數 s，用於接收用戶輸入的字串。
            int index, quantity, subtotal; //宣告整數變數 index、quantity 和 subtotal，分別用於存儲飲品的編號、數量和小計金額。

            // 進入無限迴圈，直到用戶結束訂購
            while (true)
            {
                // 提示用戶輸入飲品編號
                Console.Write("請輸入編號:");
                // 從控制台讀取用戶輸入的編號並存儲在變數 s 中
                s = Console.ReadLine();

                // 檢查是否結束訂購或輸入無效編號
                if (s == "x" || int.Parse(s) < 0 || int.Parse(s) > 5)
                {
                    // 如果輸入"x"或編號不在合理範圍內，退出訂購流程
                    Console.WriteLine("謝謝惠顧，歡迎下次再來。");
                    break;
                }
                else
                {
                    // 將從用戶輸入的編號轉換為整數並存儲在變數 index 中
                    index = int.Parse(s);
                    // 從飲品清單中獲取所選編號的飲品並存儲在變數 drink 中
                    Drink drink = drinks[index];
                    // 提示用戶輸入訂購數量
                    Console.Write("請輸入數量:");
                    // 從控制台讀取用戶輸入的數量並存儲在變數 s 中
                    s = Console.ReadLine();
                    // 檢查用戶是否輸入 'x' 或數量小於 1，如果是則退出訂購流程
                    if (s == "x" || int.Parse(s) < 1)
                    {
                        // 如果輸入"x"或數量小於1，退出訂購流程
                        Console.WriteLine("謝謝惠顧，歡迎下次再來.");
                        break;
                    }
                    else
                    {
                        quantity = int.Parse(s);
                        // 將使用者輸入的字串 (s) 解析為整數並將其儲存在 quantity 變數中，表示所訂購的飲料數量。

                        subtotal = drink.price * quantity;
                        // 計算小計（subtotal），這是所選飲料的價格（drink.price）和數量（quantity）的乘積。小計表示該訂購項目的總價格。

                        Console.WriteLine($"您訂購:{drink.size}{drink.name} {quantity}杯 每杯{drink.price}元 小計:{subtotal,5}元");
                        // 使用 Console.WriteLine 顯示訂單項目的詳細信息，包括所選飲料的名稱（drink.name）、大小（drink.size）、訂購的數量（quantity）、每杯的價格（drink.price）以及小計（subtotal）。字串插值 ($"") 用來將變數的值插入到字串中。

                        orders.Add(new OrderItem() { Index = index, Quantity = quantity, Subtotal = subtotal });
                        // 將新的訂單項目（OrderItem）添加到訂單清單（orders）中。這個項目包含所選飲料的索引（index）、數量（quantity）和小計（subtotal），這樣就記錄了每次訂購的詳細資訊。
                    }
                }
            }

        // 計算訂單總金額
        CalculateOrder(orders);
        }

        private static void show_drinks(List<Drink> drinks)
        {
            // 顯示飲料清單標題
            Console.WriteLine("飲料清單:\n");

            // 顯示飲料清單的列標題
            Console.WriteLine(string.Format("{0,-5}{1,-5}{2,-3}{3,6}", "編號", "品名", "大小杯", "價格"));

            int i = 0;

            // 顯示每種飲料的詳細資訊
            foreach (Drink drink in drinks)
            {
                // 使用 Console.WriteLine 來顯示每種飲料的資訊，包括編號（i）、品名（drink.name）、大小（drink.size）和價格（drink.price）。
                // 字串格式設定 ({0,-5}, {1,-5}, {2,-3}, {3,6}) 用來控制輸出的格式和對齊。
                Console.WriteLine($"{i,-6} {drink.name,-5} {drink.size,-3} {drink.price,5:C1}");
                i++;
            }
        }


        private static void add_drinks(List<Drink> drinks)
        {
            // 添加預設飲品到飲品清單
            drinks.Add(new Drink() { name = "紅茶", size = "大杯", price = 50 });
            drinks.Add(new Drink() { name = "紅茶", size = "小杯", price = 30 });
            drinks.Add(new Drink() { name = "綠茶", size = "大杯", price = 50 });
            drinks.Add(new Drink() { name = "綠茶", size = "小杯", price = 30 });
            drinks.Add(new Drink() { name = "咖啡", size = "大杯", price = 60 });
            drinks.Add(new Drink() { name = "咖啡", size = "小杯", price = 40 });
        }
    }
}
