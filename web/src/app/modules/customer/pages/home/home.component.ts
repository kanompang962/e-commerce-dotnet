import { Component, OnInit } from '@angular/core';
import {  Router } from '@angular/router';
import { banners, categorys, products } from 'src/assets/data/data';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit{
  
  constructor(
    private route:Router
  ){}

  banners: any[] = [];
  products: any[] = [];
  categorys: any[] = [];
  translateX = 0;

  ngOnInit(): void {
    this.categorys = categorys;
    this.products = products;
    this.banners = banners;
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
