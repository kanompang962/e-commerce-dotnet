import { Component, OnInit } from '@angular/core';
import { navlist, sociallist } from '../../../../../assets/data/data';
import { MyCartService } from 'src/app/services/my-cart/my-cart.service';
import { Router } from '@angular/router';
import { Cart } from 'src/app/models/cart.model';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit{

  constructor(
    private myCartService: MyCartService,
    private route: Router,
  ){}
  
  apiUrl = environment.apiUrl
  my_cart: Cart[] = [];
  my_cart_quantity: number = 0;
  navlist: any;
  sociallist: any;
  search_keywords:string = '';

  ngOnInit(): void {
    this.navlist = navlist
    this.sociallist = sociallist
    this.getMyCart();
  }

  login():void {
    this.route.navigate(['auth']);
  }

  getMyCart():void {
    this.myCartService.currentCart$.subscribe((state)=>{
      this.my_cart = state;
      this.my_cart_quantity = state.length;
    })
  }
  
}
