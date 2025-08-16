output "resource_group_name" {
  description = "Nome do Resource Group criado"
  value       = azurerm_resource_group.rg-postech.name
}

output "resource_group_location" {
  description = "Localização do Resource Group"
  value       = azurerm_resource_group.rg-postech.location
}

output "resource_group_id" {
  description = "ID do Resource Group"
  value       = azurerm_resource_group.rg-postech.id
}