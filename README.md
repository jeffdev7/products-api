# Products API
## API Documentation  
### Endpoints
![api-endpoints](https://github.com/user-attachments/assets/6f0ea764-aa73-450a-ac0c-2b772764a54c)

### /api/product
#### GET
<ul>
    <li><strong>Retrieve All Products</strong></li>
    <ul>
        <li><strong>Description</strong>: Retrieves a list of all products.</li>
        <li><strong>Response</strong>:</li>
        <ul>
            <li><strong>Status</strong>: 200 OK</li>
            <li><strong>Body</strong>: An array of ProductViewModel objects.</li>
        </ul>
    </ul>
</ul>

<strong>Example:</strong>
##### OK
![get-products](https://github.com/user-attachments/assets/bc401515-e3ec-47b2-9042-54e825761826)


#### POST
<ul>
    <li><strong>Creates a new product.</strong></li>
    <ul>
        <li><strong>Description</strong>: Creates a new product.</li>
        <li><strong>Response</strong>:The created ProductViewModel object with a unique id.</li>
        <ul>
            <li><strong>Status</strong>: 201 Created if successful.</li>
            <li><strong>Status</strong>: 400 Bad Request with problem details.</li>
            <li><strong>Body</strong>: A ProductViewModel object with the product details.</li>
        </ul>
    </ul>
</ul>

<strong>Example:</strong>
##### Created
![add-product-201](https://github.com/user-attachments/assets/5ae24541-f3b2-462c-a42a-6aad3bfa6162)

##### Bad Request![error-add-product](https://github.com/user-attachments/assets/c38e1458-05cd-4eb8-9291-34d52ae62ba0)

### /api/product/{id}
#### GET
<ul>
    <li><strong>Retrieves a product.</strong></li>
    <ul>
        <li><strong>Description</strong>: Retrieves the details of a specific product by its ID.</li>
        <li><strong>Response</strong>: The created ProductViewModel object with a unique id.</li>
        <li><strong>Parameter</strong>: id - The unique identifier of the product.</li>
        <ul>
            <li><strong>Status</strong>: 200 OK.</li>
            <li><strong>Status</strong>: 404 Not Found.</li>
        </ul>
    </ul>
</ul>

<strong>Example:</strong>
##### OK
![get-product-by-id](https://github.com/user-attachments/assets/37a3c6ef-165e-4e50-ba0d-2b26f7d65b51)

##### Not Found![error-get-product-by-id](https://github.com/user-attachments/assets/706de7a6-be54-449c-a7e1-f8f4f096d524)

#### PUT
<ul>
    <li><strong> Updates a product.</strong></li>
    <ul>
        <li><strong>Description</strong>:  Updates the details of an existing product.</li>
        <li><strong>Request</strong>: A ProductViewModel object .</li>
        <li><strong>Parameter</strong>: id - The unique identifier of the product.</li>
        <li><strong>Response</strong>: 200 Ok if successful.</li>
        <ul>
            <li><strong>Status</strong>: 200 OK.</li>
            <li><strong>Status</strong>: 400 Bad Request with problems details.</li>
        </ul>
    </ul>
</ul>

<strong>Example:</strong>

##### OK
![image](https://github.com/user-attachments/assets/f6f1c1c3-a09a-49d0-843d-0030c15ce85e)

##### Bad Request
![image](https://github.com/user-attachments/assets/c2b02243-4c18-4c58-802b-afc81b3cf4bb)
#### Bad Request
![image](https://github.com/user-attachments/assets/3eff8f33-af87-419c-8840-6231a0e02ed4)


#### DELETE
<ul>
    <li><strong> Deletes a product.</strong></li>
    <ul>
        <li><strong>Description</strong>:  Removes an existing product by its ID.</li>
        <li><strong>Parameter</strong>: id - The unique identifier of the product.</li>
        <li><strong>Response</strong>: 204 No Content.</li>
    </ul>
</ul>

<strong>Example:</strong>
##### No Content
![delete-product](https://github.com/user-attachments/assets/cdaa0ebe-d6fe-4b9d-bc7e-1e3e84754ec7)
