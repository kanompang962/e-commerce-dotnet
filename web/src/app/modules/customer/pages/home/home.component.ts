import { Component, OnInit } from '@angular/core';
import {  Router } from '@angular/router';
import { categorys } from 'src/assets/data/data';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit{
  
  constructor(
    private route:Router
  ){}

  categorys: any[] = [];
  translateX = 0;

  ngOnInit(): void {
    this.categorys = categorys;
  }

  productToDetail(id:number): void {
    console.log(id)
    if (id) {
      this.route.navigate(['detail']);
    }
  }


  scrollLeft() {
    this.translateX -= 100; // Adjust the scrolling distance as needed
    console.log("translateX ",this.translateX)
  }

  scrollRight() {
    if (this.translateX != 0) {
      this.translateX += 100;
    }
    console.log("translateX ",this.translateX)
  }

}
