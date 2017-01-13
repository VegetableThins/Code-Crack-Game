using System;

public class mainProgram
{
	
	//6 colors
	//4 holes
	//1 random gen array of 4 colors
	//12 guesses
	//white peg = right color, wrong spot
	//black peg = right color, right spot
    static public void Main ()
    {
		ConsoleColor[] colors = (ConsoleColor[]) ConsoleColor.GetValues(typeof(ConsoleColor));
		Console.ForegroundColor = colors[7];
		Game theGame = new Game();
		Console.Read();
    }
}

public class Game{
	
	public static string[] colors = new string[6]{"red","blue","green","orange","yellow","purple"};
	
	public string difficulty;
	public int codeLength;
	public int guesses = 0;
	public int maxGuesses = 1;
	public colorPeg[] theCode;
	
	public int whitePegs = 0;
	public int blackPegs = 0;
	
	public Random rnd = new Random();
	
	public Game(){
		//generate new code
		//check if user code is = generated code
		//give white or black peg feedback based on array matches
		//user wins if entered code = generated code
		startScreen();
		codeLength = chooseDifficulty();
		Console.WriteLine(codeLength);
		generateCode(codeLength);
		do{
			guess();
		} while (guesses != maxGuesses);
		Console.WriteLine("You are out of guesses!!");
		newGame();
		
		
	}
	
	public void startScreen(){
		logo();
		Console.WriteLine("Mastermind is a game about guessing a code.");
		Console.WriteLine("The code may consist of 6 different colors and may contain repeats.");
		Console.WriteLine("Try to guess the code by typing the four colors in sequence with a single space in between.");
		Console.WriteLine("The four colors are red, blue, green, orange, yellow and purple.");
		Console.WriteLine("If you get a black peg it means one 'peg' is in the right spot and is the right color.");
		Console.WriteLine("If you get a white peg it means one 'peg' is the right color but in the wrong spot.");
		Console.WriteLine("Try to guess within {0} tries!",maxGuesses);
		Console.WriteLine("Good Luck!");
		Console.Write("Press Enter...");
		Console.Read();
		Console.Read();
		Console.WriteLine("");
	}
	
	public void logo(){
//		Console.WriteLine("   _____                   __                  _____  .__            .___");
//		Console.WriteLine("  /     \\ _____    _______/  |_  ___________  /     \\ |__| ____    __| _/");
//		Console.WriteLine(" /  \\ /  \\\\__  \\  /  ___/\\   __\\/ __ \\_  __ \\/  \\ /  \\|  |/    \\  / __ | ");
//		Console.WriteLine("/    Y    \\/ __ \\_\\___ \\  |  | \\  ___/|  | \\/    Y    \\  |   |  \\/ /_/ | ");
//		Console.WriteLine("\\____|__  (____  /____  > |__|  \\___  >__|  \\____|__  /__|___|  /\\____ | ");
//		Console.WriteLine("        \\/     \\/     \\/            \\/              \\/        \\/      \\/ ");
//		Console.WriteLine("");
	}
	
	public void updateGameView (){
		Console.Clear();
		logo();
		Console.WriteLine("Difficulty = {0}		Guesses = {1}		Max Guesses = {2}",difficulty,guesses,maxGuesses);
		Console.WriteLine("BlackPegs = {0}			WhitePegs = {1}",blackPegs,whitePegs);
		Console.WriteLine(""); 
		Console.Write("The Code: ");
		for(int i = 0; i < codeLength; i++){
			Console.Write("{0},",theCode[i].color);
		}
		Console.WriteLine("");
		
	}
	
	public int chooseDifficulty(){
		
		Console.WriteLine("Please enter a number between 1 and 3");
		Console.WriteLine("1 = Easy");
		Console.WriteLine("2 = Medium");
		Console.WriteLine("3 = Hard");
		Console.Write("Difficulty = ");
		
		int userDifficulty = Convert.ToInt32(Console.ReadLine());
		
		switch (userDifficulty){
		case 1:
			difficulty = "Easy";
			return 4;
		case 2:
			difficulty = "Medium";
			return 6;
		case 3:
			difficulty = "Hard";
			return 8;
		default:
			difficulty = "Easy";
			return 4;
        }
	}
	
	public void generateCode(int codeLength){
		theCode = new colorPeg[codeLength];
		for(int i = 0; i < codeLength; i++){
			theCode[i] = new colorPeg(colors[rnd.Next(0,6)],i,guesses);
		} 
	}
	
	public void guess (){
		
		updateGameView();
		
		string[] guessString = (Console.ReadLine()).Split(' ');
		if(guessString.Length == codeLength){
			blackPegs = 0;
			whitePegs = 0;
			int index = 0;
			bool pegSwitch = false;
			foreach(string color in guessString){
				for(int i = 0; i < codeLength; i++){
					if(color == theCode[i].color && index == i){
						blackPegs++;
						pegSwitch = true;
					}
				}
				if(!pegSwitch && color == theCode[index].color){
					whitePegs++;
				}
				index++;
			}
			guesses++;
			if(blackPegs == codeLength){
				updateGameView();
				Console.WriteLine("Victory!!");
				Console.WriteLine("You finished the game in {0} guesses!",guesses);
				Console.Write("Press any key to Continue...");
				Console.ReadKey();
				newGame();
			}else{
				updateGameView();
			}
		}else{
			Console.WriteLine("Please enter a guess that is {0} words long",codeLength);
		} 
	}
	
	public void newGame () {
		Console.WriteLine("Would you like to play again?");
		Console.Write("Y/N: ");
		string decision = Console.ReadLine();
		if(decision == "y" || decision == "Y"){
			Console.Clear();
			Game theGame = new Game();
		}else if(decision == "n" || decision == "N"){
			Console.WriteLine("Thank you for playing!");
			Console.Write("Press any key to exit...");
			Console.ReadKey();
			Environment.Exit(0);
		}else{
			Console.WriteLine("Please Enter either Yes(Y) or No(N)");
			Console.Write("Press any key to Continue...");
			Console.ReadKey();
			newGame();
		}
	}
	
	
}

public struct colorPeg{
	
	public string color;
	public int x, y;
	
	public colorPeg(string c, int x, int y){
		this.color = c;
		this.x = x;
		this.y = y;
	
	}
}