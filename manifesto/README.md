## Passos para Inicialização do Cluster com Fastfood

### Pré-requisitos

- KIND instalado (`go install sigs.k8s.io/kind@latest`)
- `kubectl` instalado
- `podman` (para gerar a imagem)
- PowerShell (para uso de alias)

---

### Comandos para provisionar o ambiente


**Atenção:** Gere a imagem com o `podman` e exporte o arquivo `.tar` para a raiz da pasta "kind" antes de importar no KIND (step 2).

# Para ambientes de Nuvem (geralmente com LoadBalancer)
$url = "https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.10.1/deploy/static/provider/cloud/deploy.yaml"

# Para ambientes Bare-Metal / On-Premise (geralmente com NodePort)
# $url = "https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.10.1/deploy/static/provider/baremetal/deploy.yaml"

# Certifique-se de usar a versão mais recente em vez de 'controller-v1.10.1'
# Verifique em: https://github.com/kubernetes/ingress-nginx/releases

Invoke-WebRequest -Uri $url -OutFile .\nginx-ingress-controller.yaml
kubectl apply -f .\nginx-ingress-controller.yaml

Precisa adicionar no arquivo hosts 
exemplo de caminho do arquivo C:\Windows\System32\drivers\etc
127.0.0.1   fastfood

```bash
## Comandos que usei para criar os Builds


docker build -t localhost/fastfood-db:latest ./src/Soat.Eleven.FastFood.Infra/
docker build --target migrator -t localhost/fastfood-migrator:latest .
docker build --target final -t localhost/fastfood-app:latest .

## Comandos para gerar os build no formato .tar para que consigamos usar em ambientes local 

docker save -o fastfood-app.tar localhost/fastfood-app:latest
docker save -o fastfood-db.tar localhost/fastfood-db:latest
docker save -o fastfood-migrator.tar localhost/fastfood-migrator:latest

## Aqui criamos o cluster a partir de um arquivo

kind create cluster --name fastfood-cluster --config ./manifesto/kind-config.yaml

## aqui a gente carrega as imagens para dentro do cluster recem criado 

kind load image-archive fastfood-migrator.tar --name fastfood-cluster
kind load image-archive fastfood-app.tar --name fastfood-cluster
kind load image-archive fastfood-db.tar --name fastfood-cluster


Set-Alias -Name k -Value kubectl

#2 Aqui damos o apply diretamente no cluster criado , caso queira aplicar de uma vez vou colocar um marcadores indicando onde parar para que a aplicação funcione corretamente
k apply -f ./manifesto/metrics-server-kind.yaml

k apply -f ./manifesto/ingress-80.yaml

k get pods -n ingress-nginx -w

k apply -f ./manifesto/fastfood-namespace.yaml

k apply -f ./manifesto/secret.yaml

k apply -f ./manifesto/config-map.yaml

k apply -f ./manifesto/db-pvc.yaml

k apply -f ./manifesto/db-service.yaml

k apply -f ./manifesto/db.yaml

k apply -f ./manifesto/migrator-job.yaml

k get pods -n fastfood -w
## Recomendo esperar o job do migrator acabar para que o banco tenha criado todas as tabelas antes de subir aplicação  k get pods -n fastfood -w

k apply -f ./manifesto/fastfood-service.yaml

k apply -f ./manifesto/fastfood-ingress.yaml

k apply -f ./manifesto/fastfood.yaml

k apply -f ./manifesto/fastfood-hpa.yaml

k get pods -n fastfood -w


## Caso não consiga usar Set-Alias -Name k -Value kubectl

kubectl apply -f ./manifesto/metrics-server-kubectlind.yaml

kubectl apply -f ./manifesto/ingress-8080.yaml

kubectl apply -f ./manifesto/fastfood-namespace.yaml

kubectl apply -f ./manifesto/secret.yaml

kubectl apply -f ./manifesto/config-map.yaml

kubectl apply -f ./manifesto/db-pvc.yaml

kubectl apply -f ./manifesto/db-service.yaml

kubectl apply -f ./manifesto/db.yaml

kubectl apply -f ./manifesto/migrator-job.yaml
## Recomendo esperar o job do migrator acabar para que o banco tenha criado todas as tabelas antes de subir aplicação

kubectl apply -f ./manifesto/fastfood-service.yaml

kubectl apply -f ./manifesto/fastfood-ingress.yaml

kubectl apply -f ./manifesto/fastfood.yaml

kubectl apply -f ./manifesto/fastfood-hpa.yaml

Acesse 
http://fastfood/swagger

## Aqui está comando para limpar seu ambiente de trabalho
kind delete cluster --name fastfood-cluster
