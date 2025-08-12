﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Navigation;
using static System.Formats.Asn1.AsnWriter;

namespace Snake_C_
{
   public class Snake
    {
        public Color snakeColor { get; set; }
        public Head head { get; set; } 
        public List<Point> body { get; set; }
        public static string direction { get; set; }  

        public Snake(Color S_color, Point S_HeadPosition, List<Point> S_body) 
        {
            this.snakeColor = S_color;
            this.head = new Head(S_HeadPosition); 
            this.body = S_body;
        }
        public class Head
        {
            public Point Position { get; set; }          
            public Point LeftEyePosition { get; set; } 
            public Point RightEyePosition { get; set; }

            public Head(Point position)
            {
                this.Position = position;             
            }
        }
        public static Snake Move(Snake moveSnake, DataGridView dataGridView, Point positionFood, Form1 Form_1)
        {             
                Point movedHead = moveSnake.head.Position;
                Point newPositionHead = new Point();
                List<Point> newBody = new List<Point>();
                Snake movedSnake = new Snake(moveSnake.snakeColor, newPositionHead, newBody);
            try
            {
                switch (Snake.direction)
                {
                    case "Up":
                        newPositionHead = new Point(movedHead.X - 1, movedHead.Y);
                        break;
                    case "Down":
                        newPositionHead = new Point(movedHead.X + 1, movedHead.Y);
                        break;
                    case "Right":
                        newPositionHead = new Point(movedHead.X, movedHead.Y + 1);
                        break;
                    case "Left":
                        newPositionHead = new Point(movedHead.X, movedHead.Y - 1);
                        break;
                }
                if (newPositionHead.X < 0 || newPositionHead.Y > dataGridView.ColumnCount - 1 ||
                newPositionHead.Y < 0 || newPositionHead.X > dataGridView.Rows.Count - 1)
                {
                    Form_1.gameOver();
                    return moveSnake;
                }
                newBody.Add(movedHead);
                for (int i = 0; i < moveSnake.body.Count() - 1; i++)
                {
                    newBody.Add(moveSnake.body[i]);
                }
                movedSnake = new Snake(moveSnake.snakeColor, newPositionHead, newBody);
                dataGridView.Rows[newPositionHead.X].Cells[newPositionHead.Y].Style.BackColor = moveSnake.snakeColor;
                foreach (Point part in newBody)
                {
                    dataGridView.Rows[part.X].Cells[part.Y].Style.BackColor = moveSnake.snakeColor;
                }
                dataGridView.Rows[moveSnake.body[moveSnake.body.Count - 1].X].Cells[moveSnake.body[moveSnake.body.Count - 1].Y].Style.BackColor = Color.Black;
                if (newPositionHead == positionFood)
                {
                    Form_1.playSimpleSound("eat_food");
                    movedSnake.body.Add(moveSnake.body[moveSnake.body.Count - 1]);
                    movedSnake.snakeColor = Form1.New_Color;
                    Form1.foodExist = false;
                    Form1.score++;
                    Form1.The_Food = null;
                    Form1.createFood(dataGridView);
                }
            }
            catch (Exception ex) { MessageBox.Show($"Oh my GOD ! an error : {ex}  in the snake.Move Method!"); }
            return movedSnake;           
        }  
    }   
}
