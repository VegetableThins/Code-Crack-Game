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
		Game theGame = new Game();
		Console.Read();
    }
}

public class Game{
	
	public static string[] colors = new string[6]{"red","blue","green","orange","yellow","purple"};
	
	public int codeLength;
	public int guesses = 0;
	public int maxGuesses = 12;
	public int whitePegs = 0;
	public int blackPegs = 0;
	public colorPeg[] theCode;
	
	public Random rnd = new Random();
	
	public Game(){
		//generate new code
		//check if user code is = generated code
		//give white or black peg feedback based on array matches
		//user wins if entered code = generated code
		startScreen();
		codeLength = chooseDifficulty();
		Console.Clear();
		generateCode(codeLength);
		Console.WriteLine("The Code: {0},{1},{2},{3}",theCode[0].color,theCode[1].color,theCode[2].color,theCode[3].color);
		do{
			guess();
		} while (guesses != maxGuesses);
		
		
	}
	
	public void startScreen(){
		logo();
		Console.WriteLine("Mastermind is a game about guessing a code");
		Console.WriteLine("The code may consist of 6 different colors and may contain repeats");
		Console.Write("Press Enter...");
		Console.Read();
		Console.Read();
		Console.WriteLine("");
	}
	
	public void logo(){
		Console.WriteLine("   _____                   __                  _____  .__            .___");
		Console.WriteLine("  /     \\ _____    _______/  |_  ___________  /     \\ |__| ____    __| _/");
		Console.WriteLine(" /  \\ /  \\\\__  \\  /  ___/\\   __\\/ __ \\_  __ \\/  \\ /  \\|  |/    \\  / __ | ");
		Console.WriteLine("/    Y    \\/ __ \\_\\___ \\  |  | \\  ___/|  | \\/    Y    \\  |   |  \\/ /_/ | ");
		Console.WriteLine("\\____|__  (____  /____  > |__|  \\___  >__|  \\____|__  /__|___|  /\\____ | ");
		Console.WriteLine("        \\/     \\/     \\/            \\/              \\/        \\/      \\/ ");
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
//			Console.WriteLine("Difficulty set to easy");
			return 4;
		case 2:
//			Console.WriteLine("Difficulty set to medium");
			return 6;
		case 3:
//			Console.WriteLine("Difficulty set to hard");
			return 8;
		default:
//			Console.WriteLine("Difficulty set to easy");
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
		
		string[] guessString = (Console.ReadLine()).Split(' ');
		if(guessString.Length == codeLength){
			//Compare the user string to the code array somehow
			//output the black or white pegs
			//create victory/defeat conditions
			int index = 0;
			foreach(string color in guessString){
				for(int i = 0; i < codeLength; i++){
					Console.WriteLine(i);
					if(color == theCode[i].color && index == i){
						Console.WriteLine("Black Peg");
					}
				}
			index++;
			}
			guesses++;
		}else{
			Console.WriteLine("Please enter a guess that is {0} words long",codeLength);
		}
		Console.Read();
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