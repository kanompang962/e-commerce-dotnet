import { Component, OnInit } from '@angular/core';
import { Order } from 'src/app/models/order.model';
import { OrderService } from 'src/app/services/order/order.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss']
})
export class AccountComponent implements OnInit{
  
  constructor(
    private orderService:OrderService
  ){}

  order:Order[] = [];
  apiUrl = environment.apiUrl;

  ngOnInit(): void {
    this.fetchOrder();
  }

  fetchOrder():void {
    this.orderService.getOrderByUser().subscribe((res)=>{
      this.order = res;
    })
  }

}
