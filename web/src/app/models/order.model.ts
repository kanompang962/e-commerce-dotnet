
export class Order {
  payment: boolean;
  statusId: number;
  orderProducts: { productId: number, quantity: number }[];
  
  constructor(payment: boolean, statusId: number, orderProducts: { productId: number, quantity: number }[]) {
    this.payment = payment;
    this.statusId = statusId;
    this.orderProducts = orderProducts;
  }
}

  