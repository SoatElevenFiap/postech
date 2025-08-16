# Create a resource group
resource "azurerm_resource_group" "rg-postech" {
  name     = var.fiap_base_rg_name
  location = "Central US"

  tags = {
    Environment = "dev"
    Project     = "FastFood-System"
    CreatedBy   = "Terraform"
  }
}

resource "azurerm_storage_account" "tfstate" {
  name                     = var.storage_account_name
  resource_group_name      = azurerm_resource_group.rg-postech.name
  location                 = azurerm_resource_group.rg-postech.location
  account_tier             = "Standard"
  account_replication_type = "LRS"

  tags = {
    Environment = "dev"
    Project     = "FastFood-System"
    CreatedBy   = "Terraform"
  }
}

resource "azurerm_storage_container" "tfstate" {
  name                  = var.terraform_storage_container_name
  storage_account_name  = azurerm_storage_account.tfstate.name
  container_access_type = "private"
}
