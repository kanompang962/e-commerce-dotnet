import { Component, OnInit } from '@angular/core';
import { navlist, sociallist } from '../../../../../assets/data/data';
import { MyCartService } from 'src/app/services/my-cart/my-cart.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit{

  constructor(
    private myCartService: MyCartService,
  ){}
  
  my_cart: number = 0;
  navlist: any;
  sociallist: any;

  ngOnInit(): void {
    this.navlist = navlist
    this.sociallist = sociallist
    this.getMyCart();
  }

  getMyCart(): void {
    this.myCartService.currentCart$.subscribe((state)=>{
      this.my_cart = state.length;
    })
  }
  
}
