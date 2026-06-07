import pandas as pd
import matplotlib.pyplot as plt
import numpy as np

ws = pd.read_csv('../data/ws_results.csv')

ws_avg = ws.groupby('p').agg(
    avg_L=('avg_path_length', 'mean'),
    avg_C=('clustering_coefficient', 'mean')
).reset_index()

ws_avg['L_norm'] = ws_avg['avg_L'] / ws_avg['avg_L'].max()
ws_avg['C_norm'] = ws_avg['avg_C'] / ws_avg['avg_C'].max()

plt.figure(figsize=(8, 5))
plt.semilogx(ws_avg['p'], ws_avg['L_norm'], 'b-o', label='L(p) / L(0)')
plt.semilogx(ws_avg['p'], ws_avg['C_norm'], 'r-s', label='C(p) / C(0)')
plt.xlabel('Rewiring Probability p')
plt.ylabel('Normalized Value')
plt.title('Watts-Strogatz Small-World Properties')
plt.legend()
plt.grid(True)
plt.savefig('../data/graphs/ws_plot.png', dpi=150)
plt.show()

k = pd.read_csv('../data/kleinberg.csv')

k_success = k[k['hops'] != -1]
k_failed = k[k['hops'] == -1]

k_avg = k_success.groupby('r').agg(
    avg_hops = ('hops', 'mean')
).reset_index()

total_per_r = k.groupby('r').size()
failed_per_r = k_failed.groupby('r').size().reindex(total_per_r.index, fill_value=0)
failure_rate = (failed_per_r / total_per_r).reset_index()
failure_rate.columns = ['r', 'failure_rate']

plt.figure(figsize=(8, 5))
plt.plot(k_avg['r'], k_avg['avg_hops'], 'b-o', label = 'Average Hops')
plt.axvline(x=2, color='r', linestyle ='--', label='r=2 (theoretical optimum)')
plt.xlabel('Exponent r')
plt.ylabel('Average Hops')
plt.title('Kleinberg Greedy Routing Performance (Grid)')
plt.legend()
plt.grid(True)
plt.savefig('../data/graphs/kleinberg_plot.png', dpi=150)
plt.show()

kt = pd.read_csv('../data/kleinberg_torus.csv')

kt_success = kt[kt['hops'] != -1]
kt_failed = kt[kt['hops'] == -1]
    
kt_avg = kt_success.groupby('r').agg(
    avg_hops = ('hops', 'mean')
).reset_index()

total_per_r = kt.groupby('r').size()
failed_per_r = kt_failed.groupby('r').size().reindex(total_per_r.index, fill_value=0)
failure_rate = (failed_per_r / total_per_r).reset_index()
failure_rate.columns = ['r', 'failure_rate']

plt.figure(figsize=(8, 5))
plt.plot(kt_avg['r'], kt_avg['avg_hops'], 'b-o', label = 'Average Hops')
plt.axvline(x=2, color='r', linestyle ='--', label='r=2 (theoretical optimum)')
plt.xlabel('Exponent r')
plt.ylabel('Average Hops')
plt.title('Kleinberg Greedy Routing Performance (Torus)')
plt.legend()
plt.grid(True)
plt.savefig('../data/graphs/kleinberg_torus_plot.png', dpi=150)
plt.show()


# Flat vs Torus overlay
k_flat = pd.read_csv('../data/kleinberg.csv')
k_torus = pd.read_csv('../data/kleinberg_torus.csv')

k_flat_avg = k_flat[k_flat['hops'] != -1].groupby('r').agg(avg_hops=('hops', 'mean')).reset_index()
k_torus_avg = k_torus[k_torus['hops'] != -1].groupby('r').agg(avg_hops=('hops', 'mean')).reset_index()

plt.figure(figsize=(8, 5))
plt.plot(k_flat_avg['r'], k_flat_avg['avg_hops'], 'b-o', label='Flat Grid')
plt.plot(k_torus_avg['r'], k_torus_avg['avg_hops'], 'r-s', label='Torus Grid')
plt.axvline(x=2, color='gray', linestyle='--', label='r=2 (theoretical optimum)')
plt.xlabel('Exponent r')
plt.ylabel('Average Hops')
plt.title('Flat vs Torus Greedy Routing Comparison')
plt.legend()
plt.grid(True)
plt.savefig('../data/graphs/flat_vs_torus.png', dpi=150)
plt.show()

flat = pd.read_csv('../data/kleinberg_scaling_flat.csv')
torus = pd.read_csv('../data/kleinberg_scaling_torus.csv')

flat['grid_type'] = 'flat'
torus['grid_type'] = 'torus'

df = pd.concat([flat, torus], ignore_index=True)

# filter out failed routing attempts
df_success = df[df['hops'] != -1]

# average hops per n and r value
scaling = df_success.groupby(['n', 'r', 'grid_type']).agg(
    avg_hops=('hops', 'mean'),
    std_hops=('hops', 'std')
).reset_index()

# plot flat grid
fig, axes = plt.subplots(1, 2, figsize=(14, 5))

for grid, ax in zip(['flat', 'torus'], axes):
    data = scaling[scaling['grid_type'] == grid]
    for r in sorted(data['r'].unique()):
        r_data = data[data['r'] == r]
        ax.plot(r_data['n'], r_data['avg_hops'], marker='o', label=f'r={r}')
        ax.fill_between(
            r_data['n'],
            r_data['avg_hops'] - r_data['std_hops'],
            r_data['avg_hops'] + r_data['std_hops'],
            alpha=0.1
        )
    ax.set_xlabel('Grid size n')
    ax.set_ylabel('Average Hops')
    ax.set_title(f'Scaling with n ({grid} grid)')
    ax.legend()
    ax.grid(True)

plt.tight_layout()
plt.savefig('../data/graphs/scaling_plot.png', dpi=150)
plt.show()


# Check if growth matches log^2(n)
fig, axes = plt.subplots(1, 2, figsize=(14, 5))

for grid, ax in zip(['flat', 'torus'], axes):
    data = scaling[scaling['grid_type'] == grid]
    
    # calculate log^2(n) for each n value
    n_values = sorted(data['n'].unique())
    log2n = [np.log(n)**2 for n in n_values]
    
    for r in sorted(data['r'].unique()):
        r_data = data[data['r'] == r].sort_values('n')
        ax.plot(log2n, r_data['avg_hops'], marker='o', label=f'r={r}')
    
    ax.set_xlabel('log²(n)')
    ax.set_ylabel('Average Hops')
    ax.set_title(f'Hops vs log²(n) ({grid} grid)')
    ax.legend()
    ax.grid(True)

plt.tight_layout()
plt.savefig('../data/scaling_log2_plot.png', dpi=150)
plt.show()

r_theory = np.linspace(0.01, 5.5, 1000)
y_theory = np.where(r_theory < 2, (2 - r_theory) / 3, (r_theory - 2) / (r_theory - 1))

# normalize empirical results to same scale for overlay
k_flat_norm = k_flat_avg.copy()
k_flat_norm['avg_hops_norm'] = (k_flat_norm['avg_hops'] - k_flat_norm['avg_hops'].min()) / (k_flat_norm['avg_hops'].max() - k_flat_norm['avg_hops'].min())

plt.figure(figsize=(8, 5))
plt.plot(r_theory, y_theory, 'k-', linewidth=2, label='Theoretical lower bound')
plt.plot(k_flat_norm['r'], k_flat_norm['avg_hops_norm'], 'b-o', label='Empirical (flat grid)')
plt.axvline(x=2, color='gray', linestyle='--', alpha=0.5)
plt.xlabel('Exponent r')
plt.ylabel('Normalized value')
plt.title('Theoretical vs Empirical Routing Performance')
plt.legend()
plt.grid(True)
plt.savefig('../data/graphs/theoretical_vs_empirical.png', dpi=150)
plt.show()