import { Component, OnInit } from '@angular/core';
import {  Router } from '@angular/router';
import { Product } from 'src/app/models/product.model';
import { ProductService } from 'src/app/services/product/product.service';
import { banners, categorys } from 'src/assets/data/data';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit{
  
  constructor(
    private route:Router,
    private productService:ProductService,
  ){}

  apiUrl = environment.apiUrl
  banners: any[] = [];
  products: Product[] = [];
  categorys: any[] = [];
  translateX = 0;

  ngOnInit(): void {
    this.fetchProducts();
    this.categorys = categorys;
    // this.products = products;
    this.banners = banners;
  }

  fetchProducts():void {
    this.productService.getProductAll().subscribe((res)=>{
      this.products = res;
    })
  }

  productToDetail(item:any): void {
    if (item) {
      this.route.navigate(['detail', item.id]);
    }
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
