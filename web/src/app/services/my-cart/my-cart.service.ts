import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

interface CartItem {
  data: any; 
  quantity: number;
}

@Injectable({
  providedIn: 'root'
})
export class MyCartService {

  private _currentCart= new BehaviorSubject<any[]>([]);
  currentCart$ = this._currentCart.asObservable();

  setMyCart(newItem:any, quantity:number) {
    if (newItem && quantity > 0) {
      const existingItem = this._currentCart.value.filter((c)=>c.data.id == newItem.id? c.quantity = c.quantity += quantity : null);

      if (existingItem.length < 1) {
        const formatItem = {
          data: newItem,
          quantity: quantity
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
