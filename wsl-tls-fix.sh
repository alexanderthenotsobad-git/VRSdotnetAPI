#!/bin/bash

echo "=== WSL Network TLS Fix ==="
echo ""

# Step 1: Disable IPv6
echo "Step 1: Disabling IPv6 (common WSL issue)..."
sudo sysctl -w net.ipv6.conf.all.disable_ipv6=1
sudo sysctl -w net.ipv6.conf.default.disable_ipv6=1
sudo sysctl -w net.ipv6.conf.lo.disable_ipv6=1
echo "✓ IPv6 disabled"
echo ""

# Step 2: Fix MTU size
echo "Step 2: Adjusting MTU size..."
sudo ip link set dev eth0 mtu 1400
echo "✓ MTU set to 1400"
echo ""

# Step 3: Configure DNS
echo "Step 3: Configuring DNS..."
sudo bash -c 'cat > /etc/resolv.conf << EOF
nameserver 8.8.8.8
nameserver 1.1.1.1
EOF'
echo "✓ DNS configured"
echo ""

# Step 4: Test connectivity
echo "Step 4: Testing with IPv4 only..."
curl -4 -I https://api.nuget.org/v3/index.json
echo ""

# Step 5: Configure dotnet
echo "Step 5: Configuring .NET for IPv4..."
echo 'export DOTNET_SYSTEM_NET_HTTP_SOCKETSHTTPHANDLER_ENABLEIPV6=0' >> ~/.bashrc
export DOTNET_SYSTEM_NET_HTTP_SOCKETSHTTPHANDLER_ENABLEIPV6=0
echo "✓ .NET configured for IPv4"
echo ""

echo "=== Fix Complete ==="
echo ""
echo "Now try:"
echo "  curl -4 https://api.nuget.org/v3/index.json"
echo "  dotnet restore"