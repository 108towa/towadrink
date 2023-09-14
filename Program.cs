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
            List<Drink> drinks = new List<Drink>();
            List<OrderItem> orders = new List<OrderItem>();
            add_drinks(drinks);
            show_drinks(drinks);
            orderdrink(drinks, orders);
        }
        private static void CalculateOrder(List<OrderItem> orders)
        {
            double total = 0.0;
            string message = "";
            double sellPrice = 0.0;
            Console.Write("----------------------------------------------------------------------------------------");
            foreach (OrderItem orderItem in orders) total += orderItem.Subtotal;
            if (total >= 500)
            {
                message = "訂購滿500元以上者8折";
                sellPrice = total * 0.8;
            }
            else if (total >= 300)
            {
                message = "訂購滿300元以上者85折";
                sellPrice = total * 0.85;
            }
            else if (total >= 200)
            {
                message = "訂購滿200元以上者9折";
                sellPrice = total * 0.9;
            }
            else
            {
                message = "訂購未滿200元不打折";
                sellPrice = total;
            }
            Console.WriteLine($"\n您總共訂購{orders.Count}項飲料 總計{total}元。{message} 總計需付款{sellPrice}元。");
            Console.Write("----------------------------------------------------------------------------------------");
        }
        private static void orderdrink(List<Drink> drinks, List<OrderItem> orders)
        {
            Console.WriteLine("\n請開始訂購，按下X鍵結束");
            string s;
            int index, quantity, subtotal;
            while (true)
            {
                Console.Write("請輸入編號:");
                s = Console.ReadLine();
                
                if (s == "x" || int.Parse(s) < 0 || int.Parse(s) > 5)
                {
                    Console.WriteLine("謝謝惠顧，歡迎下次再來。");
                    break;
                }
                else
                {
                    index = int.Parse(s);
                    Drink drink = drinks[index];
                    Console.Write("請輸入數量:");
                    s = Console.ReadLine();
                    if (s == "x" || int.Parse(s) < 1)
                    {
                        Console.WriteLine("謝謝惠顧，歡迎下次再來。");
                        break;
                    }
                    else
                    {
                        quantity = int.Parse(s);
                        subtotal = drink.price * quantity;
                        Console.WriteLine($"您訂購:{drink.size}{drink.name} {quantity}杯 每杯{drink.price}元 小計:{subtotal,5}元");
                        orders.Add(new OrderItem() { Index = index, Quantity = quantity, Subtotal = subtotal });
                    }
                }

            }
            CalculateOrder(orders);
        }

        private static void show_drinks(List<Drink> drinks)
        {
            /*for(int i= 0; i < drinks.Count; i++)
            {
                Console.WriteLine($"飲料名稱:{drinks[i].name} 尺寸:{drinks[i].size} 價格:{drinks[i].price}");
            }*/
            Console.WriteLine("飲料清單:\n");
            //Console.WriteLine($"{"品名",-5}{"大小杯",3}{"價格",6}");
            Console.WriteLine(string.Format("{0,-5}{1,-5}{2,-3}{3,6}", "編號", "品名", "大小杯", "價格"));
            int i = 0;
            foreach (Drink drink in drinks)
            {
                Console.WriteLine($"{i,-6} {drink.name,-5} {drink.size,-3} {drink.price,5:C1}");
                i++;
            }
        }
        private static void add_drinks(List<Drink> drinks)
        {
            drinks.Add(new Drink() { name = "紅茶", size = "大杯", price = 50 });
            drinks.Add(new Drink() { name = "紅茶", size = "小杯", price = 30 });
            drinks.Add(new Drink() { name = "綠茶", size = "大杯", price = 50 });
            drinks.Add(new Drink() { name = "綠茶", size = "小杯", price = 30 });
            drinks.Add(new Drink() { name = "咖啡", size = "大杯", price = 60 });
            drinks.Add(new Drink() { name = "咖啡", size = "小杯", price = 40 });
        }
    }
}
