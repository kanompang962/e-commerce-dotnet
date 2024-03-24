import { Component, OnInit } from '@angular/core';
import { Cart } from 'src/app/models/cart.model';
import { MyCartService } from 'src/app/services/my-cart/my-cart.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit{

  constructor(
    private myCartService: MyCartService,
  ){}
  
  apiUrl = environment.apiUrl
  my_cart: Cart[] = [];
  my_cart_quantity: number = 0;
  total_price: number = 0;

  ngOnInit(): void {
    this.getMyCart();
  }
  

  getMyCart():void {
    this.myCartService.currentCart$.subscribe((state)=>{
      this.my_cart = state;
      this.my_cart_quantity = state.length;
      this.calculatorTotalPrice();
    })
  }

  decrease(i:number):void {
    if (this.my_cart[i].quantity > 1) {
        this.my_cart[i].quantity -= 1;
        this.calculatorTotalPrice();
    }
   }
 
   increase(i:number):void {
      this.my_cart[i].quantity += 1;
      this.calculatorTotalPrice();
   }

   checkedBox(i:number):void {
      this.my_cart[i].checked = !this.my_cart[i].checked;
      console.log(this.my_cart[i])
      this.calculatorTotalPrice();
   }

   calculatorTotalPrice():void {
    this.total_price = 0;
      this.my_cart.forEach((c) => {
        if (c.checked) {
          this.total_price += c.data.price * c.quantity; 
        }
      })
   }
}
