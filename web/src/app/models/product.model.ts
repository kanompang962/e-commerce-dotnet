import { Category } from "./category.model";

export class Product {
  id: number;
  name: string;
  img: string;
  price: number;
  discountPercentage: number;
  rating: number;
  quantity: number;
  description: string;
  category: Category;

  constructor(data: any) {
    this.id = data.id;
    this.name = data.name;
    this.img = data.img;
    this.price = data.price;
    this.discountPercentage = data.discountPercentage;
    this.rating = data.rating;
    this.quantity = data.quantity;
    this.description = data.description;
    this.category = new Category(data.category);
  }
}