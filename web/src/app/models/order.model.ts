import { Product } from "./product.model";
import { Status } from "./status.model";

export class Order {
  id: number;
  date: Date;
  totalAmount: number;
  totalPrice: number;
  payment: boolean;
  statusId: number;
  status: Status;
  orderProducts: { 
    productId: number, 
    quantity: number,
    products: Product,
  }[];
  
  constructor(
    id: number,
    date: Date,
    totalAmount: number,
    totalPrice: number,
    payment: boolean,
    statusId: number,
    status: Status,
    orderProducts: { 
      productId: number, 
      quantity: number,
      products: Product,
    }[]
  ) {
    this.id = id;
    this.date = date;
    this.totalAmount = totalAmount;
    this.totalPrice = totalPrice;
    this.payment = payment;
    this.statusId = statusId;
    this.status = status;
    this.orderProducts = orderProducts;
  }
}

  