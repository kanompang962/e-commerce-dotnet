import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Order } from 'src/app/models/order.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http: HttpClient) {}

  createOrder(payload:Order): Observable<Order> {
    return this.http.post<Order>(`/api/order`,payload);
  }

  getOrderByUser(): Observable<Order[]> {
    return this.http.get<Order[]>(`/api/order/user`);
  }

  getCart(): Observable<Order> {
    return this.http.get<Order>(`/api/order/cart`);
  }
}
