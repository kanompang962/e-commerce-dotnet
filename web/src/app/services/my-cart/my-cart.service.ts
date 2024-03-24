import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Cart } from 'src/app/models/cart.model';
import { Product } from 'src/app/models/product.model';

interface CartItem {
  data: any; 
  quantity: number;
}

@Injectable({
  providedIn: 'root'
})
export class MyCartService {

  private _currentCart= new BehaviorSubject<Cart[]>([
    // {
    //   data: {
    //     "id": 7,
    //     "title": "Samsung Galaxy Book",
    //     "description": "Samsung Galaxy Book S (2020) Laptop With Intel Lakefield Chip, 8GB of RAM Launched",
    //     "price": 1499,
    //     "discountPercentage": 4.15,
    //     "rating": 4.25,
    //     "stock": 50,
    //     "brand": "Samsung",
    //     "category": "laptops",
    //     "thumbnail": "https://cdn.dummyjson.com/product-images/7/thumbnail.jpg",
    //     "images": [
    //       "https://cdn.dummyjson.com/product-images/7/1.jpg",
    //       "https://cdn.dummyjson.com/product-images/7/2.jpg",
    //       "https://cdn.dummyjson.com/product-images/7/3.jpg",
    //       "https://cdn.dummyjson.com/product-images/7/thumbnail.jpg"
    //     ]
    //   },
    //   quantity:10,
    //   checked: true
    // },
    // {
    //   data: {
    //     "id": 8,
    //     "title": "Microsoft Surface Laptop 4",
    //     "description": "Style and speed. Stand out on HD video calls backed by Studio Mics. Capture ideas on the vibrant touchscreen.",
    //     "price": 1499,
    //     "discountPercentage": 10.23,
    //     "rating": 4.43,
    //     "stock": 68,
    //     "brand": "Microsoft Surface",
    //     "category": "laptops",
    //     "thumbnail": "https://cdn.dummyjson.com/product-images/8/thumbnail.jpg",
    //     "images": [
    //       "https://cdn.dummyjson.com/product-images/8/1.jpg",
    //       "https://cdn.dummyjson.com/product-images/8/2.jpg",
    //       "https://cdn.dummyjson.com/product-images/8/3.jpg",
    //       "https://cdn.dummyjson.com/product-images/8/4.jpg",
    //       "https://cdn.dummyjson.com/product-images/8/thumbnail.jpg"
    //     ]
    //   },
    //   quantity:5,
    //   checked: false
    // }
  ]);
  currentCart$ = this._currentCart.asObservable();

  setMyCart(newItem:Product, quantity:number) {
    if (newItem && quantity > 0) {
      const existingItem = this._currentCart.value.filter((c)=>c.data.id == newItem.id? c.quantity = c.quantity += quantity : null);

      if (existingItem.length < 1) {
        const formatItem = {
          data: newItem,
          quantity: quantity,
          checked: false
        };
  
        const updatedCart = [...this._currentCart.value, formatItem];
        this._currentCart.next(updatedCart);
        console.log(existingItem.length)
      }
      return true;
      
    }else {
      return false;
    }
  }
}
