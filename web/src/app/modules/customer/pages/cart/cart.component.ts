import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Cart } from 'src/app/models/cart.model';
import { Order } from 'src/app/models/order.model';
import { DialogService } from 'src/app/services/dialog/dialog.service';
import { MyCartService } from 'src/app/services/my-cart/my-cart.service';
import { OrderService } from 'src/app/services/order/order.service';
import { ProductService } from 'src/app/services/product/product.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit{

  constructor(
    private myCartService: MyCartService,
    private productService: ProductService,
    private orderService: OrderService,
    private dialogService: DialogService,
    private route: Router,
    private router: ActivatedRoute
  ){}
  
  apiUrl = environment.apiUrl
  id:number | undefined;
  my_cart: Cart[] = [];
  my_cart_quantity: number = 0;
  total_price: number = 0;
  order:Order | any;

  ngOnInit(): void {
    this.router.queryParams.subscribe(params => {
      if (params['data']) {
        this.id = params['data'];
        this.fetchProduct(this.id!);
      } else{
        this.getMyCart();
      }
    });
  }

  checkout():void {
    this.order = null;
    const extractedData = this.my_cart
    .filter(item => item.checked)
    .map(item => ({
        productId: item.data.id,
        quantity: item.quantity
    }));

    if (extractedData.length != 0) {
      this.order = {
        payment: true,
        statusId: 1,
        orderProducts: extractedData
      }
    }

    if (this.order != null) {
      this.orderService.createOrder(this.order).subscribe((res)=>{
        if (res) {
          this.dialogService.openDialogAlert('');
          this.route.navigate(['/']);
        }
      })
    }

    console.log(this.order)
  }

  getMyCart():void {
    this.myCartService.currentCart$.subscribe((state)=>{
      this.my_cart = state;
      this.my_cart_quantity = state.length;
      this.calculatorTotalPrice();
    })
  }

  fetchProduct(id:number):void {
    this.productService.getProductById(id).subscribe((res)=>{
      const data = {
        data: res,
        quantity: 1,
        checked: true,
      }
      this.my_cart.push(data);
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
