import { Component, OnInit } from '@angular/core';
import { about_shopee, app, customer_service, follow_us, logistics, payment, related_products } from 'src/assets/data/data';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit{

  related_products:any[] = [];
  customer_service:any[] = [];
  about_shopee:any[] = [];
  logistics:any[] = [];
  follow_us:any[] = [];
  payment:any[] = [];
  app:any[] = [];

  ngOnInit(): void {
    this.related_products = related_products;
    this.customer_service = customer_service;
    this.about_shopee = about_shopee;
    this.logistics = logistics;
    this.follow_us = follow_us;
    this.payment = payment;
    this.app = app;
  }

}
