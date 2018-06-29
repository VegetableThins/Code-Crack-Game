using System;
using System.Collections.Generic;
using System.Linq;

public class mainProgram
{
    static public void Main ()
    {
		ConsoleColor[] colors = (ConsoleColor[]) ConsoleColor.GetValues(typeof(ConsoleColor));
		Console.ForegroundColor = colors[7];
        Game theGame = new Game();
        Console.Read();
    }
}

public class Game{
	static public int totalEarnings = 0;
	public int codeLength;
	public int guesses = 0;
	public int maxGuesses = 15;
    public List<string> allGuesses;
	public string[] theCode;
	
	public int correctNumbers = 0;
	
	public Random rnd = new Random();
	
	public Game(){
        //generate new code
        //check if user code is = generated code
        //give feedback based on array matches
        //user wins if entered code = generated code
        allGuesses = new List<string>();

        startScreen();
		codeLength = chooseDifficulty();
		Console.WriteLine(codeLength);
		generateCode(codeLength);
        updateGameView();
        do
        {
			guess();
		} while (guesses != maxGuesses);
		Console.WriteLine("You are out of guesses...");
        Console.Write("Continue...");
        Console.ReadKey();
        Console.WriteLine("The vault remains sealed...");
        Console.Write("Continue...");
        Console.ReadKey();
        Console.Write("The Code was: ");
        for (int i = 0; i < theCode.Length; i++)
        {
            Console.Write("{0}", theCode[i]);
        }
        Console.Write("\n");
        newGame();
		
		
	}

    public void logo()
    {
         Console.WriteLine("____   ____            .__   __    _________                       __    ");
         Console.WriteLine("\\   \\ /   /____   __ __|  |_/  |_  \\_   ___ \\____________    ____ |  | __");
         Console.WriteLine(" \\   Y   /\\__  \\ |  |  \\  |\\   __\\ /    \\  \\/\\_  __ \\__  \\ _/ ___\\|  |/ /");
         Console.WriteLine("  \\     /  / __ \\|  |  /  |_|  |   \\     \\____|  | \\// __ \\\\  \\___|    < ");
         Console.WriteLine("   \\___/  (____  /____/|____/__|    \\______  /|__|  (____  /\\___  >__|_ \\");
         Console.WriteLine("               \\/                          \\/            \\/     \\/     \\/");
         Console.WriteLine("");
    }

    public void startScreen(){
		logo();
		Console.WriteLine("Vault Crack is a game about cracking a code.");
		Console.WriteLine("The code may consist of 7 different numbers (ranging from 0-6) and may contain repeats.");
		Console.WriteLine("The higher the difficulty the longer the code.");
		Console.WriteLine("The correct tally counts the number of correct numbers in your code.");
        Console.WriteLine("Get all the numbers in the code correct and you've busted the vault.");
        Console.WriteLine("The higher the difficult, the greater the loot!");
        Console.WriteLine("Try to guess within {0} tries!",maxGuesses);
		Console.WriteLine("Good Luck!");
		Console.Write("Press Enter");
        Console.ReadKey();
		Console.WriteLine("");
	}

    public int chooseDifficulty()
    {

        Console.WriteLine("Please enter a number between 1 and 3");
        Console.WriteLine("1 = Normal (4 numbers long)");
        Console.WriteLine("2 = Hard (6 numbers long)");
        Console.WriteLine("3 = Expert (8 numbers long)");
        Console.Write("Difficulty = ");

        string userDifficulty = Console.ReadLine();
        int value;
        if (int.TryParse(userDifficulty, out value))
        {

            switch (value)
            {
                case 1:
                    return 4;
                case 2:
                    return 6;
                case 3:
                    return 8;
                default:
                    return 4;
            }
        }
        else
        {
            return 4;
        }
    }

    public void generateCode(int codeLength)
    {
        theCode = new string[codeLength];
        for (int i = 0; i < codeLength; i++)
        {
            theCode[i] = rnd.Next(1, 6).ToString();
        }
    }
	
	public void guess (){
		
        Console.Write("Guess: ");
        string guessString = Console.ReadLine();
        Char[] guessArray = guessString.ToCharArray();
 
        if (guessArray.Length == codeLength && guessArray.All(char.IsDigit)){
            correctNumbers = 0;
			int index = 0;

			foreach(char entry in guessArray)
            {
                if (theCode[index].Contains(entry))
                {
                    correctNumbers++;
                }
                index++;
			}

            string newGuess = String.Join(" ", guessString);
            newGuess = String.Format("  Guess {0} = ",guesses+1) + newGuess + String.Format(" || CorrectNumbers: {0} ", correctNumbers);

            allGuesses.Add(newGuess);

			guesses++;
			if(correctNumbers == codeLength){
				updateGameView();
                vaultCracked();
			}else{
				updateGameView();
			}
		}else{
            updateGameView(true);
		} 
	}

    public void updateGameView(bool inputError = false)
    {
        Console.Clear();
        logo();
        
        Console.Write("The code = {0}",theCode);
        Console.Write("Total Vault Earnings = {0}",totalEarnings);
        Console.WriteLine(" ");
        Console.WriteLine(" ");

        Console.WriteLine("Code Length = {0}		Guesses = {1}		Max Guesses = {2}", codeLength, guesses, maxGuesses);
        Console.WriteLine("Correct #s = {0}		Number Range = 1 - 6", correctNumbers);
        Console.WriteLine("");

        if (inputError)
        {
            Console.WriteLine("Make sure your guess is all numbers and is {0} characters long!", codeLength);
        }else
        {
            Console.WriteLine("Current Guesses: ");
        }
        foreach (string guess in allGuesses)
        {
            Console.WriteLine(guess);
        }
        Console.WriteLine("");

    }

    public void vaultCracked()
    {
        Console.WriteLine("Victory!!");
        Console.Write("The Code was: ");
        for (int i = 0; i < theCode.Length; i++)
        {
            Console.Write("{0}", theCode[i]);
        }
        Console.Write("\n");
        Console.WriteLine("You cracked the code in {0} guesse(s)!", guesses);
        Console.Write("Press any key to Continue");
        Console.ReadKey();
        //calculate loot gained
        //based on difficulty and randomization
        Loot theLoot = new Loot(codeLength);
        theLoot.display();
        totalEarnings += theLoot.getTotalValue();
        //asked user to play again
        newGame();
    }

    public void newGame () {
        Console.WriteLine(" ");
		Console.WriteLine("Would you like to play again?");
		Console.Write("Y/N: ");
		string decision = Console.ReadLine();
		if(decision == "y" || decision == "Y"){
			Console.Clear();
			Game theGame = new Game();
		}else if(decision == "n" || decision == "N"){
			Console.WriteLine("Thank you for playing!");
			Console.Write("Press any key to exit");
			Console.ReadKey();
			Environment.Exit(0);
		}else{
			Console.WriteLine("Please Enter either Yes(Y) or No(N)");
			Console.Write("Press any key to Continue");
			Console.ReadKey();
			newGame();
		}
	}
	
	
}

public class Loot
{

    public int totalValue, numNecklaces,numRings,numDiamonds,numRubies,numEmeralds,money;

    public int necklaceValue,ringValue,diamondValue,rubyValue,emeraldValue;

    public int difficultyBonus;

    public Random rnd = new Random();

    public Loot(int difficutlty)
    {
        totalValue = 0;
        necklaceValue = 1500;
        ringValue = 1000;
        diamondValue = 500;
        rubyValue = 300;
        emeraldValue = 150;
        numNecklaces = 0;
        numRings = 0;
        numDiamonds = 0;
        numRubies = 0;
        numEmeralds = 0;
        money = 0;

        if (difficutlty == 6)
        {
            difficultyBonus = 5;
        }
        else if (difficutlty == 8)
        {
            difficultyBonus = 10;
        }
        else
        {
            difficultyBonus = 0;
        }

        //drop chances
        //necklace 1% ring 5% diamond 10% ruby 20% emerald 30% cash 100%
        for (int i = 0; i < (difficutlty * 2); i++)
        {
            int chance = rnd.Next(1, 100);
            if (chance == (100 - difficultyBonus))
            {
                //necklace
                numNecklaces++;
                totalValue = totalValue + necklaceValue;
            }
            else if (chance >= (95 - difficultyBonus))
            {
                //ring
                numRings++;
                totalValue = totalValue + ringValue;
            }
            else if (chance >= (90 - difficultyBonus))
            {
                //diamonds
                numDiamonds++;
                totalValue = totalValue + diamondValue;
            }
            else if (chance >= (80 - difficultyBonus))
            {
                //rubies
                numRubies++;
                totalValue = totalValue + rubyValue;
            }
            else if (chance >= (70 - difficultyBonus))
            {
                numEmeralds++;
                totalValue = totalValue + emeraldValue;
            }
            else
            {
                int newmoney = rnd.Next(1, 100 + (difficultyBonus));
                money = money + newmoney;
                totalValue = totalValue + newmoney;
            }
        }
    }
    
    public int getTotalValue(){
        return totalValue;
    }

    public void display()
    {
        Console.WriteLine(" ");
        Console.WriteLine(" ");
        Console.WriteLine("The Vault Contained a total of ${0}.",totalValue);
        Console.WriteLine(" ");
        //output vault content
        if(numNecklaces != 0){
            Console.WriteLine(" {0} Necklace(s) : ${1}",numNecklaces,(numNecklaces*necklaceValue));
        }
        if(numRings != 0){
            Console.WriteLine(" {0} Ring(s)     : ${1}",numRings,(numRings*ringValue));
        }
        if(numDiamonds != 0){
            Console.WriteLine(" {0} Diamond(s)  : ${1}",numDiamonds,(numDiamonds*diamondValue));
        }
        if(numRubies != 0){
            Console.WriteLine(" {0} Ruby(s)     : ${1}",numRubies,(numRubies*rubyValue));
        }
        if(numEmeralds != 0){
            Console.WriteLine(" {0} Emerald(s)  : ${1}",numEmeralds,(numEmeralds*emeraldValue));
        }
        if(money != 0){
            Console.WriteLine("     Cash        : ${0}",money);
        }
        Console.WriteLine(" ");
        Console.Write("press any key to Continue");
        Console.ReadKey();
    }
}

       

public class Valuable
{
    public string Type;
    public int Value;
    public Random rnd = new Random();
    public Valuable(int valuableType)
    {
        switch (valuableType)
        {
            case 0:
                Type = "necklace";
                Value = 1500;
                break;
            case 1:
                Type = "ring";
                Value = 1000;
                break;
            case 2:
                Type = "diamond";
                Value = 500;
                break;
            case 3:
                Type = "ruby";
                Value = 200;
                break;
            case 4:
                Type = "emerald";
                Value = 100;
                break;
            case 5:
                Type = "cash";
                Value = rnd.Next(1, 100);
                break;
            default:
                Type = "cash";
                Value = rnd.Next(1, 100);
                break;
        }
    }
}