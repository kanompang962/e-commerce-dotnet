import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { take } from 'rxjs';
import { Product } from 'src/app/models/product.model';
import { DialogService } from 'src/app/services/dialog/dialog.service';
import { MyCartService } from 'src/app/services/my-cart/my-cart.service';
import { ProductService } from 'src/app/services/product/product.service';
import { categorys } from 'src/assets/data/data';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit{

  constructor(
    private activatedRoute: ActivatedRoute,
    private myCartService: MyCartService,
    private dialogService: DialogService,
    private productService: ProductService,
    private route: Router,
    ) { }

  // product:any;
  apiUrl = environment.apiUrl
  product?:Product;
  translateX = 0;
  quantity:number = 1;

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params: Params) => {
      const id = params['id']; 
      this.fetchProduct(id);
      // this.product = products.find((p)=>p.id == id);
    });
  }

  fetchProduct(id:number):void {
    this.productService.getProductById(id).subscribe((res)=>{
      this.product = res;
    })
  }

  decrease():void {
   if (this.quantity > 1) {
      this.quantity -= 1;
   }
  }

  increase():void {
    this.quantity += 1;
  }

  addToCart(item: any): void {
    const res = this.myCartService.setMyCart(item, this.quantity);
    if (res) this.dialogService.openDialogAlert('');
    // this.myCartService.currentCart$.pipe(
    //   take(1) 
    // ).subscribe(currentCart => {
    //   const updatedCart = [...currentCart, item];
    //   this.myCartService.setMyCart(updatedCart);
    // });
  }

  routeToCart():void {
    this.route.navigate(['cart']);
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
