import { Product } from "./product.model";

export class Cart {
    data: Product;
    quantity: number;
    checked: boolean;
  
    constructor(data: any) {
      this.data = new Product(data.data);
      this.quantity = data.name;
      this.checked = data.checked;
    }
  }
  