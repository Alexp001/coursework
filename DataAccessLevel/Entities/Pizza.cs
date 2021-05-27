﻿namespace DataAccessLevel.Entities
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public Pizza(int id, string name, double price, string image)
        {
            Id = id;
            Name = name;
            Price = price;
            Image = image;
        }
    }
}
