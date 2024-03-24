import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from 'src/app/models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) {}

  getProductAll(): Observable<Product[]> {
    return this.http.get<Product[]>(`/api/product`);
  }

  getProductById(id:number): Observable<Product> {
    return this.http.get<Product>(`/api/product/${id}`);
  }
}
