using Microsoft.Xna.Framework.Input;

namespace game_project.Controllers
{
    class Input
    {
        static KeyboardState currentKeyState;
        static KeyboardState previousKeyState;

        public enum Inputs
        {
            UP = 0,
            DOWN,
            LEFT,
            RIGHT,
            ATTACK,
            BOMB
        }

        public static KeyboardState GetState()
        {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            return currentKeyState;
        }


        // checks if a key is pressed when it was released the previous frame
        public static bool KeyDown(Keys key)
        {
            return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
        }

        public static bool KeyDown(params Keys[] keys)
        {
            bool found = false;
            foreach (var key in keys)
            {
                if (currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key))
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        // checks if a key is released when it was pressed the previous frame
        public static bool KeyUp(Keys key)
        {
            return !currentKeyState.IsKeyDown(key) && previousKeyState.IsKeyDown(key);
        }

        public static bool Down(Inputs input)
        {
            var keyboard = Keyboard.GetState();
            bool result = false;

            switch (input)
            {
                case Inputs.LEFT:
                    result = keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left);
                    break;
                case Inputs.RIGHT:
                    result = keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right);
                    break;
                case Inputs.UP:
                    result = keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up);
                    break;
                case Inputs.DOWN:
                    result = keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down);
                    break;
                case Inputs.ATTACK:
                    result = keyboard.IsKeyDown(Keys.Z) || keyboard.IsKeyDown(Keys.N);
                    break;
                case Inputs.BOMB:
                    result = KeyDown(Keys.D1);
                    break;
            }
            return result;
        }


        public static bool Up(Inputs input)
        {
            var keyboard = Keyboard.GetState();
            bool result = false;

            switch (input)
            {
                case Inputs.LEFT:
                    result = KeyUp(Keys.A) || KeyUp(Keys.Left);
                    break;
                case Inputs.RIGHT:
                    result = KeyUp(Keys.D) || KeyUp(Keys.Right);
                    break;
                case Inputs.UP:
                    result = KeyUp(Keys.W) || KeyUp(Keys.Up);
                    break;
                case Inputs.DOWN:
                    result = KeyUp(Keys.S) || KeyUp(Keys.Down);
                    break;
                case Inputs.ATTACK:
                    result = keyboard.IsKeyDown(Keys.Z) || keyboard.IsKeyDown(Keys.N);
                    break;
            }
            return result;
        }

    }
}
