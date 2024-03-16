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

  setMyCart(newItem:any) {
    if (newItem) {
      const existingItem = this._currentCart.value.filter((c)=>c.data.id == newItem.id? c.quantity = c.quantity += 1 : null);
      if (existingItem.length < 1) {
        const formatItem = {
          data: newItem,
          quantity: 1
        };
  
        const updatedCart = [...this._currentCart.value, formatItem];
        this._currentCart.next(updatedCart);
      }


      console.log(this._currentCart.value)
    }
  }
}
