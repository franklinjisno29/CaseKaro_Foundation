Feature: SearchProduct
Background: User will be on Homepage

@E@E-Search_Product
Scenario: Search Product and Click Add To Cart Button
	When User clicks on the search button
	* Fills the '<searchText>'
	Then Search Result Page is loaded in the same page
	When User clicks the '<productNo>' product
	Then Product Page is loaded in the same page
	When User clicks the Add to Cart Button
	Then Add To Cart Page is loaded in the same page
Examples:
	| searchText | productNo |
	| iphone     | 1		 |
