
namespace TextRpg
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다!");

            Console.Write("\n원하시는 이름을 설정해주세요: ");
            string playerName = Console.ReadLine();
            if (playerName == string.Empty)
            {
                Console.Clear();
                Console.WriteLine("이름을 확인해주세요.엔터를 눌러주세요.");
                Console.ReadLine();
                Console.Clear();
                Main();
            }
            Console.Clear();
            string jobName = "";
            while (true)
            {
                Console.WriteLine("원하시는 직업의 번호를 입력해주세요.");
                Console.WriteLine("1. 전사  2. 마법사  3. 궁수");
                switch (Console.ReadLine())
                {
                    case "1":
                        jobName = "전사";
                        Console.Clear();
                        Console.WriteLine($"{jobName}가 되신것을 축하합니다.엔터를 눌러 마을로 가세요.");
                        Console.ReadLine(); 
                        break;
                    case "2":
                        jobName = "마법사";
                        Console.Clear();
                        Console.WriteLine($"{jobName}가 되신것을 축하합니다.엔터를 눌러 마을로 가세요.");
                        Console.ReadLine();
                        break;
                    case "3":
                        jobName = "궁수";
                        Console.Clear();
                        Console.WriteLine($"{jobName}가 되신것을 축하합니다.엔터를 눌러 마을로 가세요.");
                        Console.ReadLine();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("올바른 숫자를 입력해주세요.엔터를 눌러주세요.");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                }
                break;
            }

            Character player = new Character(playerName, jobName);
            List<Item> shopItems = new List<Item>
        {
            new Item("나무 갑옷", 0, 5, "나무로 만든 갑옷이다.쓰다 버려라.", 500),
            new Item("나무 검", 7, 0, "나무로 만든 검이다.쓰다 버려라.", 700),
            new Item("나무 지팡이", 7, 0, "나무로 만든 지팡이다.쓰다 버려라.", 700),
            new Item("나무 활", 7, 0, "나무로 만든 활이다.쓰다 버려라.", 700),
            new Item("골든 검", 20, 0, "금으로 만든 검이다.쓰다 버리지마라.", 5000),
            new Item("골든 지팡이", 20, 0, "금으로 만든 지팡이다.쓰다 버리지마라.", 5000),
            new Item("골든 활", 20, 0, "금으로 만든 활이다.쓰다 버리지마라.", 5000)
        };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 것을 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기  2. 인벤토리  3. 상점  4. 던전 (개발 중)");
                Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");

                switch (Console.ReadLine())
                {
                    case "1": 
                        player.ShowStatus(); break;
                    case "2": 
                        player.ShowInventory(); player.ManageEquipment(); break;
                    case "3": 
                        player.OpenShop(shopItems); break;
                    case "4": 
                        Console.WriteLine("\n던전 시스템이 개발 중입니다."); break;
                    default: 
                        Console.WriteLine("잘못된 입력입니다.엔터를 눌러주세요."); break;
                }
                Console.ReadLine();
            }
        }
    }
    public class Item
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int AttackBoost { get; private set; }
        public int DefenseBoost { get; private set; }
        public int Price { get; private set; }
        public bool IsEquipped { get; set; }
        public bool IsPurchased { get; set; }

        public Item(string name, int attackBoost, int defenseBoost, string description, int price)
        {
            Name = name;
            AttackBoost = attackBoost;
            DefenseBoost = defenseBoost;
            Description = description;
            Price = price;
            IsEquipped = false;
            IsPurchased = false;
        }
    }

    public class Character
    {
        public int Level { get; private set; } = 1;
        public string Name { get; private set; }
        public string Job { get; private set; }
        public int BaseAttack { get; private set; } = 10;
        public int BaseDefense { get; private set; } = 5;
        public int Health { get; private set; } = 100;
        public int Gold { get; set; } = 1500;
        private List<Item> inventory = new List<Item>();

        public Character(string name, string job)
        {
            Name = name;
            Job = job;
        }

        public void ShowStatus()
        {
            Console.Clear();
            int totalAttack = BaseAttack + GetEquippedStat("Attack");
            int totalDefense = BaseDefense + GetEquippedStat("Defense");

            Console.WriteLine("===== [ 상태 보기 ] =====");
            Console.WriteLine($"Lv. {Level}");
            Console.WriteLine($"{Name} ( {Job} )");
            Console.WriteLine($"공격력 : {totalAttack} (+{GetEquippedStat("Attack")})");
            Console.WriteLine($"방어력 : {totalDefense} (+{GetEquippedStat("Defense")})");
            Console.WriteLine($"체 력 : {Health}");
            Console.WriteLine($"Gold : {Gold} G\n");
        }

        private int GetEquippedStat(string type)
        {
            int total = 0;
            foreach (var item in inventory)
            {
                if (item.IsEquipped)
                {
                    total += (type == "Attack") ? item.AttackBoost : item.DefenseBoost;
                }
            }
            return total;
        }

        public void ShowInventory()
        {
            Console.Clear();
            Console.WriteLine("===== [ 인벤토리 ] =====");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            if (inventory.Count == 0)
            {
                Console.WriteLine("현재 보유 중인 아이템이 없습니다.\n");
            }
            else
            {
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < inventory.Count; i++)
                {
                    string equippedMark = inventory[i].IsEquipped ? "[E] " : "";
                    Console.WriteLine($"- {i + 1} {equippedMark}{inventory[i].Name} | 공격력 +{inventory[i].AttackBoost} | 방어력 +{inventory[i].DefenseBoost} | {inventory[i].Description}");
                }
            }
            Console.WriteLine("0. 나가기");
        }

        public void ManageEquipment()
        {
            while (true)
            {
                ShowInventory();
                Console.Write("\n장착/해제할 아이템 번호를 입력하세요 (0: 나가기): ");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > inventory.Count)
                {
                    if (choice == 0)
                    {
                        Console.WriteLine("엔터를 눌러주세요.");
                        break;
                    }
                    Console.WriteLine("잘못된 입력입니다.엔터를 눌러주세요.");
                    Console.ReadLine();
                    continue;
                }

                inventory[choice - 1].IsEquipped = !inventory[choice - 1].IsEquipped;
                Console.WriteLine($"\n▶ {inventory[choice - 1].Name} {(inventory[choice - 1].IsEquipped ? "장착" : "해제")}되었습니다.");
            }
        }

        public void OpenShop(List<Item> shopItems)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== [ 상점 ] =====");
                Console.WriteLine($"[보유 골드] {Gold} G\n");

                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < shopItems.Count; i++)
                {
                    string status = shopItems[i].IsPurchased ? "구매완료" : $"{shopItems[i].Price} G";
                    Console.WriteLine($"- {i + 1} {shopItems[i].Name} | 공격력 +{shopItems[i].AttackBoost} | 방어력 +{shopItems[i].DefenseBoost} | {shopItems[i].Description} | {status}");
                }

                Console.WriteLine("\n0. 나가기");
                Console.Write("\n구매할 아이템 번호를 입력하세요: ");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > shopItems.Count)
                {
                    if (choice == 0) break;
                    Console.WriteLine("잘못된 입력입니다.엔터를 눌러주세요.");
                    Console.ReadLine();
                    continue;
                }

                BuyItem(shopItems[choice - 1]);
            }
        }

        public void BuyItem(Item item)
        {
            if (item.IsPurchased)
            {
                Console.WriteLine("\n이미 구매한 아이템입니다.엔터를 눌러주세요.");
                Console.ReadLine();
                return;
            }

            if (Gold < item.Price)
            {
                Console.WriteLine("\nGold 가 부족합니다.엔터를 눌러주세요.");
                Console.ReadLine();
                return;
            }

            Gold -= item.Price;
            inventory.Add(item);
            item.IsPurchased = true;
            Console.WriteLine($"\n▶ {item.Name}을(를) 구매했습니다!");
            Console.ReadLine();
        }
    }
}