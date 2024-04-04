import { Component, OnInit } from '@angular/core';
import { navlist, sociallist } from '../../../../../assets/data/data';
import { MyCartService } from 'src/app/services/my-cart/my-cart.service';
import { Router } from '@angular/router';
import { Cart } from 'src/app/models/cart.model';
import { environment } from 'src/environments/environment';
import { User } from 'src/app/models/user.model';
import { getSession, removeSession } from 'src/app/core/session/sessionService';

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
  user:User | undefined;
  my_cart: Cart[] = [];
  my_cart_quantity: number = 0;
  navlist: any;
  sociallist: any;
  search_keywords:string = '';

  ngOnInit(): void {
    this.navlist = navlist
    this.sociallist = sociallist
    this.getUser();
    this.getMyCart();
  }

  login():void {
    this.route.navigate(['auth']);
  }

  logout():void {
    const res = removeSession('user');
    if (res) {
      this.route.navigate(['/']);
    }
  }

  account():void {
    this.route.navigate(['account']);
  }

  getUser():void {
    this.user = getSession('user');
    // if (!this.user) {
    //   this.route.navigate(['auth']);
    // }
  }

  getMyCart():void {
    this.myCartService.currentCart$.subscribe((state)=>{
      this.my_cart = state;
      this.my_cart_quantity = state.length;
    })
  }

  viewMyShoppingCart():void {
    this.route.navigate(['cart']);
  }
  
}
