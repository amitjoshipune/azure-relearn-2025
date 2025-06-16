You're now almost done with a full **Kubernetes-hosted, JWT-secured, observable .NET 8 Web API** — great job! Let's now summarize:

---

## ✅ FINAL KUBERNETES COMMANDS – DEPLOY, INSPECT, LOGS

### 🔁 1. **Build and Push Docker Image**

You likely already did this, but here’s the clean sequence:

```bash
# Navigate to API project folder
cd D:\amit\azure-relearn-2025\src\AzureRelearn.WebApi

# Build Docker image
docker build -t amitjoshi/azure-relearn-api:v1 .

# Push to DockerHub
docker push amitjoshi/azure-relearn-api:v1
```

Make sure this image name (`amitjoshi/azure-relearn-api:v1`) matches your Kubernetes `deployment.yaml`.

---

### 🚀 2. **Apply Deployment and Service to Kubernetes**

Assuming you're in the root folder where `k8s/deployment.yaml` and `k8s/service.yaml` are:

```bash
cd D:\amit\azure-relearn-2025\k8s

# Deploy API
kubectl apply -f deployment.yaml
kubectl apply -f service.yaml
```

---

### 🧠 3. **Understand K8s Hierarchy**

```
Kubernetes Cluster
│
├── Node (virtual machine: Minikube, AKS node, etc.)
│   └── Pod (smallest deployable unit)
│       └── Container (your .NET app, inside Docker)
│
├── Deployment (manages Pods)
│
├── Service (stable access to Pods via ClusterIP / NodePort / LoadBalancer)
│
└── Ingress (optional: API Gateway-style access)
```

---

### 🔍 4. **Inspect Cluster Status**

```bash
# Get all objects
kubectl get all

# Just pods
kubectl get pods

# Just services
kubectl get svc

# Just deployments
kubectl get deployments

# Detailed pod info
kubectl describe pod <pod-name>

# Example
kubectl describe pod azure-relearn-api-xyz
```

---

### 📄 5. **View Logs**

```bash
# Get pod name first
kubectl get pods

# Then get logs
kubectl logs <pod-name>

# Example
kubectl logs azure-relearn-api-6855bc48c6-9npd6
```

If you’re using structured logging or OpenTelemetry `console` exporter, you’ll see logs and traces here.

---

### 🔁 6. **Update Deployment (e.g., v2)**

```bash
# Build and push v2 image
docker build -t amitjoshi/azure-relearn-api:v2 .
docker push amitjoshi/azure-relearn-api:v2

# Update deployment.yaml to use v2
# then reapply
kubectl apply -f deployment.yaml
```

---

## 🌐 7. **Access API via NodePort**

```bash
minikube service azure-relearn-api-service
```

⬆️ This opens the API in the browser (auto-fetches NodePort + IP).

Alternatively:

```bash
minikube ip  # e.g., 192.168.49.2
```

If your service exposes `8080:30081`, call it at:

```bash
http://192.168.49.2:30081/swagger/index.html
```

---

### 📦 8. **Call API from HTML + See Logs**

When you:

* Call your `/api/token/login` from the HTML client,
* Use the token to call `/api/weatherforecast`,

Then:

```bash
kubectl logs <pod-name>
```

You'll see:

* Console logs (`LoggingMiddleware`)
* Exception logs (if any)
* OpenTelemetry traces (if console exporter enabled)

---

## ✅ Bonus: Cleanup

```bash
kubectl delete -f deployment.yaml
kubectl delete -f service.yaml
```

---

Let me know when you want:

* Ingress + custom domain routing
* Multiple microservices + JWT passthrough
* Distributed tracing with Jaeger, Zipkin, or Azure Monitor

You’ve nailed this phase — just a few more days of this pace and you’ll be *production-grade architecture interview-ready* 🔥
