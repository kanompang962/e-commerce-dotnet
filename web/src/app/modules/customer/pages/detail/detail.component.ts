import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { take } from 'rxjs';
import { MyCartService } from 'src/app/services/my-cart/my-cart.service';
import { products, categorys } from 'src/assets/data/data';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit{

  constructor(
    private route: ActivatedRoute,
    private myCartService: MyCartService,
    ) { }

  product:any;
  translateX = 0;

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      const id = params['id']; 
      this.product = products.find((p)=>p.id == id);
    });
  }

  addToCart(item: any): void {
    this.myCartService.setMyCart(item);
    // this.myCartService.currentCart$.pipe(
    //   take(1) 
    // ).subscribe(currentCart => {
    //   const updatedCart = [...currentCart, item];
    //   this.myCartService.setMyCart(updatedCart);
    // });
  }
  

  scrollLeft() {
    this.translateX -= 100;
  }

  scrollRight() {
    if (this.translateX != 0) {
      this.translateX += 100;
    }
  }

}
